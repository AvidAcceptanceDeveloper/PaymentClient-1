using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace RDDSMakePayments
{

    public partial class RDSSMakePaymentParent : Form
    {
        bool LoanLoaded = false;
        string CurrentState { get; set; }
        public string LogonToken { get; set; }
        public string LogonId { get; set; }
        public string NLSUserId { get; set; }
        public Form SendingForm { get; set; }

        RadButtonElement rbtnOpenPayClient = new RadButtonElement();

        public RDSSMakePaymentParent()
        {
            InitializeComponent();

            rbtnOpenPayClient.Text = "Open First Mile";
            rbtnOpenPayClient.MinSize = new System.Drawing.Size(100, 24);
            rbtnOpenPayClient.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            rbtnOpenPayClient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rbtnOpenPayClient.Click += new System.EventHandler(this.rbtnOpenPayClient_Click);
            this.rbtnOpenPayClient.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            this.rWizMakeAPayment.CommandArea.CommandElements.Add(rbtnOpenPayClient);
            //this.rbtnOpenPayClient.Click +=rbtnOpenPayClient_Click;

            rWizMakeAPayment.Next += new Telerik.WinControls.UI.WizardCancelEventHandler(rWizMakeAPayment_Next);
            rWizMakeAPayment.Cancel += new EventHandler(rWizMakeAPayment_Cancel);
            
        }

        

        public bool ValidateCustomerDemoFields()
        {
            if (rtxtPayerName.Text.Trim().Length == 0)
            {
                rlblPayerName.ForeColor = System.Drawing.Color.Red;
               
                return false;
            }
            else
            {
                rlblPayerName.ForeColor = System.Drawing.Color.Black;
            }
            if (rtxtAddress1.Text.Trim().Length == 0)
            {
                rlblAddress1.ForeColor = System.Drawing.Color.Red;
                
                return false;
            }
            else
            {
                rlblAddress1.ForeColor = System.Drawing.Color.Black;
            }
            if (rtxtCity.Text.Trim().Length == 0)
            {
                rlblCity.ForeColor = System.Drawing.Color.Red;
               
                return false;
            }
            else
            {
                rlblCity.ForeColor = System.Drawing.Color.Black;
            }
            if (rddlState.Text.Trim().Length == 0)
            {
                rlblState.ForeColor = System.Drawing.Color.Red;
               
                return false;
            }
            else
            {
                rlblState.ForeColor = System.Drawing.Color.Black;
            }
            if (rtxtZip.Text.Trim().Length == 0)
            {
                rlblZip.ForeColor = System.Drawing.Color.Red;
               
                return false;
            }
            else
            {
                rlblZip.ForeColor = System.Drawing.Color.Black;
            }
           
            return true;
        }
        private bool ValidateCCCheckFields()
        {
           
            if (rpvPaymethod.SelectedPage==rpvCCDebit)
            {
                if (rtxtCCNumber.Text.Trim().Length == 0)
                {
                    rlblCCNumber.ForeColor = System.Drawing.Color.Red;
                    
                    rtxtCCNumber.Focus();
                    return false;
                }
                else
                {
                    rlblCCNumber.ForeColor = System.Drawing.Color.Black;
                }
                //Total amount must be greater than 0;
                if (rtxtTotalAmount.Text.Length == 0)
                {
                    rtxtTotalAmount.ForeColor = System.Drawing.Color.Red;
                    rtxtTotalAmount.Focus();
                    return false;
                }
                else
                {
                    rtxtTotalAmount.ForeColor = System.Drawing.Color.Black;
                }
                
            }
            if (rpvPaymethod.SelectedPage==rpvCheck)
            {
                if (rtxtABANumber.Text.Trim().Length == 0)
                {
                    rlblABANumber.ForeColor = System.Drawing.Color.Red;
                   
                    rtxtABANumber.Focus();
                    return false;
                }
                else
                {
                    rlblABANumber.ForeColor = System.Drawing.Color.Black;
                }
                if (rtxtBankAccount.Text.Trim().Length == 0)
                {
                    rlblBankAccount.ForeColor = System.Drawing.Color.Red;
                    
                    rtxtBankAccount.Focus();
                    return false;
                }
                else
                {
                    rlblBankAccount.ForeColor = System.Drawing.Color.Black;
                }
                if (rddlChkSav.Text.Trim().Length == 0)
                {
                    rlblChkSav.ForeColor = System.Drawing.Color.Red;

                    rddlChkSav.Focus();
                    return false;
                }
                else
                {
                    rlblChkSav.ForeColor = System.Drawing.Color.Black;
                }
               

            }
            if (rtxtTotalAmount.Text.Trim().Length == 0)
            {
                rlblTotalAmount.ForeColor = System.Drawing.Color.Red;

                rtxtTotalAmount.Focus();
                return false;
            }
            else
            {
                rlblTotalAmount.ForeColor = System.Drawing.Color.Black;
            }
            return true;
        }
        private void rWizMakeAPayment_Cancel(object sender, System.EventArgs e)
        {
            if (rWizMakeAPayment.CancelButton.Text == "Cancel")
            {
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("Do you wish to continue?", "Cancel Current Payment", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {


                    this.Dispose();
                }
                else
                {
                    return;
                }
            }
            else
            {
                this.Dispose();
            }
        }
        private string GenerateId()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        private string MakePaymentCredit(bool bCreateSubscription=false)
        {
            RDSSNLSMPUtilsClasses.cEMail oMail = null;
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile.ToString());
           

            // string MPFirstMileSettings = oSettings.MPSettings;
            string sTotalAmount = "";
            string sTotalFeeAmount = "", sMerchantReceipt = "", sCustomerDemoInfo = "";
            string sPaymentPostingEmail = "";
            string strNLSPostingErrMsg = "";
            string sNLSUserId = "";
            
            string sPaymentReference = "";
            bool IncludeFees = false;
            string[] StatusArray;
            string ExpireDte = "";

            RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            //RDSSNLSMPUtilsClasses.cAuthNet oAuthNet = new RDSSNLSMPUtilsClasses.cAuthNet();
            RDSSNLSMPUtilsClasses.cPaymentTech oPayTech = new RDSSNLSMPUtilsClasses.cPaymentTech();

           // ATSSecurePostUILib.ATSSecurePostUI PostUI = null;

            StringBuilder sb = new StringBuilder();
            StringBuilder sb_merchant = new StringBuilder();
            StringBuilder sb_customerdemo = new StringBuilder();

            string LegalEntity = nls.GetLoanGroupByLoan(txtLoanNumber.Text);


            string[] NameSplit = rtxtPayerName.Text.Split(' ');
            string sFName = NameSplit[NameSplit.GetLowerBound(0)].ToString();
            string sLName = NameSplit[NameSplit.GetUpperBound(0)].ToString();
            var results = "";

            IncludeFees = true;
           
            //PayTech vars
            //string sTransType = "A", sBin = "000001", sMerchantId = "219863", sTerminalId = "001", sApiUrl = "", sIndustryType = "MO";


           // PostUI = new ATSSecurePostUILib.ATSSecurePostUI(); //Merchant Partners First Mile Assembly
            ExpireDte =   rddlYear.SelectedItem.ToString() + rddlMonth.SelectedItem.ToString();
            sTotalAmount = rspnPayDollars.Value.ToString() + rspnPayCents.Value.ToString();
            


            //sTotalAmount = Double.Parse(sTotalAmount).ToString().Replace(".",string.Empty);

            if (bCreateSubscription == true)
            {
                results = oPayTech.CCMarkForCapture(txtLoanNumber.Text + GenerateId(), sTotalAmount, rtxtCCNumber.Text, ExpireDte,"", rdtpPaymentDate.Value.ToShortDateString());
                //results = oAuthNet.AuthNetPostDatePaymentCCDebit(oSettings.AuthNetApiLoginKey, oSettings.AuthNetTransactionKey, sFName, sLName, decimal.Parse(rtxtTotalAmount.Text.Replace("$",string.Empty)), 1, 1, rdtpPaymentDate.Value, rtxtCCNumber.Text, ExpireDte, rtxtSecCode.Text,rtxtEmail.Text );
            }
            else
            {
                results = oPayTech.CCMarkForCapture(txtLoanNumber.Text + GenerateId(), sTotalAmount, rtxtCCNumber.Text ,ExpireDte,rtxtSecCode.Text);
               // results = oAuthNet.AuthNetPayByCCDebit(oSettings.AuthNetApiLoginKey, oSettings.AuthNetTransactionKey, sFName, sLName, rtxtCCNumber.Text, ExpireDte, rtxtSecCode.Text, rtxtTotalAmount.Text,payeremail: rtxtEmail.Text );
            }
           
            if (results.ToUpper().IndexOf("ERROR")== 0)  //leave form state alone
            {
                rtxtPaymentResult.Text = results;
                return "DECLINED";
            }
            if (results.ToUpper().IndexOf("APPROVED") >= 0) //post to NLS and close form
            {

                StatusArray = results.Split('|');
                string sRetry = "";
                sPaymentReference = StatusArray[1].ToString();
                

                IncludeFees = false;  //for Avid, no fees with transactions
                

                //12-17-2014 This will not work, need to fix it and update the client
                //added while loop to handle retry
                string nlsRetVal = nls.PayByCCDebit(txtLoanNumber.Text, "", sTotalAmount, sTotalFeeAmount,rdtpPaymentDate.Value.ToShortDateString() , sPaymentReference, this.NLSUserId, IncludeFees.ToString(), results);
                while (sRetry == "")
                {
                    if (nlsRetVal == "The operation has timed out")
                    {
                        DialogResult dr = MessageBox.Show("The loan is locked.  Make sure you have save your changes and try again.", "Retry Payment", MessageBoxButtons.RetryCancel);
                        switch (dr)
                        {
                            case DialogResult.Retry:
                                sRetry = "";
                                break;
                            case DialogResult.Cancel:
                                sRetry = "Quit";
                                break;
                        }

                    }
                 else
                    {
                        sRetry = "Quit";
                    }
                }
                /////////////////////////////////////////////////////////////////////
                //put retry logic here if posting to nls is not successful
                if (nlsRetVal.IndexOf("NORTRIDGE ERROR") > 0)
                {

                    //sPaymentPostingEmail = oSettings.
                    strNLSPostingErrMsg = "An error occured posting a payment to NLS.  Loan Number: " + txtLoanNumber.Text + "  Order Id: " + sPaymentReference + " Payment Amount: " + sTotalAmount + " Collector Number: " + sNLSUserId;
                    oMail = new RDSSNLSMPUtilsClasses.cEMail();

                    //send payment processing the error so it can be manually posted to NLS.  If email will not work, show the message to the collector
                    try
                    {
                        //email can not be hard coded
                        oMail.EmailCustomerReceipt(sPaymentPostingEmail, oSettings.CustomerServiceEmail, "NORTRIDGE PAYMENT ERROR (ACH)", strNLSPostingErrMsg);
                        rtxtPaymentResult.Text = "An error occured posting a payment to Nortridge.  Email sent to payment processing.";
                    }
                    catch (Exception ex)
                    {
                        rtxtPaymentResult.Text = "Payment not posted to Nortridge.  Email to payment processing failed.  Please notify payment processing with information needed to post payment."
                            + @"\r\n" + ex.Message;
                        
                    }
                    return "ERROR";

                }
                else
                {
                    if (nlsRetVal == "True")
                    {
                        rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\n";

                    }
                    else
                    {
                        if (oSettings.QueAllPayemnts == "true")
                        {
                            rtxtPaymentResult.Text = "Payment Queued for Posting to Nortridge.";
                            RDSSNLSMPUtilsClasses.cMsgQ oMsgQ = new RDSSNLSMPUtilsClasses.cMsgQ(oSettings.PayQuePath);
                            oMsgQ.AddMessage(nlsRetVal);
                        }
                        else
                        {
                            rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\n";
                            this.Close();
                        }
                    }
                }


                rtxtPaymentResult.Text += "Transaction Reference:  " + sPaymentReference;
                
               
            }
            return "FINISH";
           
        }
        private string MakePaymentACHNacha()
        {
            RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            string CheckingOrSavings = "";
            if (rddlChkSav.SelectedItem.Text  == "Checking")
            {
                CheckingOrSavings = "0";
            }
            else
            {
                CheckingOrSavings = "1";
            }
            string nlsRetVal = nls.PayByACHNacha(txtLoanNumber.Text, "1", rtxtABANumber.Text, rtxtBankAccount.Text, rtxtTotalAmount.Text.Replace("$", String.Empty), "0", "NACHA FILE", NLSUserId, "false", "", rdtpPaymentDate.Value, rtxtEmail.Text, CheckingOrSavings);
            string[] sPaymentReference = nlsRetVal.Split('|');

            if (sPaymentReference.GetUpperBound(0) > 0)
            {
                if (sPaymentReference[0] == "True")
                {

                    rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\n";
                    rtxtPaymentResult.Text += "Amount: " + rtxtTotalAmount.Text + "\r\n";
                    rtxtPaymentResult.Text += "Reference Number: " + sPaymentReference[1];
                    

                }
                else
                {
                    rtxtPaymentResult.Text = "ERROR: ACH Payment Failed with the following error:  " + sPaymentReference[1];

                   
                }
            }
            else
            {
                return sPaymentReference[0];
            }
            
            return rtxtPaymentResult.Text;

        }
        private string MakePaymentCheck(bool bCreateSubscription = false)
        {
           
            RDSSNLSMPUtilsClasses.cEMail oMail = null;
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile.ToString());

            string[] NameSplit = rtxtPayerName.Text.Split(' ');
            string sFName = NameSplit[NameSplit.GetLowerBound(0)].ToString();
            string sLName = NameSplit[NameSplit.GetUpperBound(0)].ToString();
           
            string sTotalAmount = "", sRetry="";
            string sTotalFeeAmount = "", sMerchantReceipt = "", sCustomerDemoInfo="";
            string sPaymentPostingEmail = "";
            string strNLSPostingErrMsg = "";
            string sNLSUserId = "";
           
            string sPaymentReference = "";

            string nlsRetVal = "";
            bool IncludeFees = false;

            string[] StatusArray;
            
            RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            //RDSSNLSMPUtilsClasses.cAuthNet oAuthNet = new RDSSNLSMPUtilsClasses.cAuthNet();
            RDSSNLSMPUtilsClasses.cPaymentTech oPayTech = new RDSSNLSMPUtilsClasses.cPaymentTech();

            StringBuilder sb = new StringBuilder();
            StringBuilder sb_merchant = new StringBuilder();
            StringBuilder sb_customerdemo = new StringBuilder();

            string LegalEntity = nls.GetLoanGroupByLoan(txtLoanNumber.Text);
            string results = "";

            IncludeFees = false;



            sTotalAmount = rspnPayDollars.Value.ToString() + rspnPayCents.Value.ToString();

            //strip comma from numbers larger than 999.99
            sTotalAmount = Double.Parse(sTotalAmount).ToString();

            // // setup stuff for both credit and debit payments  -- THIS CAN NOT STAY HARD CODED
            // sMerchantReceipt = oSettings.MPReceipt;
            //PayTech vars
           // string sTransType = "A", sBin = "000001", sMerchantId = "219863", sTerminalId = "001", sApiUrl = "", sIndustryType = "MO";
            string CheckingOrSavings = "";

            if (rddlChkSav.SelectedItem.Text == "Checking")
            {
                CheckingOrSavings = "0";
            }
            else
            {
                CheckingOrSavings = "1";
            }
            if (bCreateSubscription==true)
            {
                
                results = oPayTech.ECMarkForCapture(txtLoanNumber.Text  + DateTime.Now.ToOADate(), rtxtTotalAmount.Text.Replace("$",string.Empty),  rtxtABANumber.Text, rtxtBankAccount.Text, CheckingOrSavings, rdtpPaymentDate.Value.ToShortDateString(),rtxtPayerName.Text,rtxtAddress1.Text ,rtxtCity.Text ,rddlState.SelectedText,rtxtZip.Text  );
                
            }                                        
            else{
                results = oPayTech.ECMarkForCapture(txtLoanNumber.Text  + DateTime.Now.ToOADate(), rtxtTotalAmount.Text.Replace("$", string.Empty), rtxtABANumber.Text, rtxtBankAccount.Text, CheckingOrSavings,DateTime.Today.ToShortDateString(),rtxtPayerName.Text,rtxtAddress1.Text ,rtxtCity.Text ,rddlState.SelectedText,rtxtZip.Text );
            }   

                    
                    if (results.ToUpper().IndexOf("ERROR") == 0)  //leave form state alone
                    {
                        rtxtPaymentResult.Text = results;
                        return "ERROR";
                    }
                    if (results.ToUpper().IndexOf("APPROVAL") == 0) //post to NLS and close form
                    {

                        StatusArray = results.Split('|');
                        sPaymentReference = StatusArray[1].ToString();

                        rWizMakeAPayment.BackButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
                        rWizMakeAPayment.CancelButton.Text = "Close";

                        

                       

                        while (sRetry == "" )
                        {
                            nlsRetVal = nls.PayByCheck(txtLoanNumber.Text, "", "", sTotalAmount, sTotalFeeAmount, sPaymentReference,rdtpPaymentDate.Value.ToString(), sNLSUserId, IncludeFees.ToString(), results);
                            if (nlsRetVal == "The operation has timed out")
                            {
                                DialogResult dr = MessageBox.Show("The loan is locked.  Make sure you have save your changes and try again.", "Retry Payment", MessageBoxButtons.RetryCancel);
                                switch(dr)
                                {
                                    case DialogResult.Retry:
                                        sRetry = "";
                                        break;
                                    case DialogResult.Cancel:
                                        sRetry = "Quit";
                                        break;
                                }
                                
                            }
                            else
                            {
                                sRetry = "Quit";
                            }
                        }
                        //put retry logic here if posting to nls is not successful
                        if (nlsRetVal.IndexOf("NORTRIDGE ERROR") == 0)
                        {

                            //sPaymentPostingEmail = oSettings.
                            strNLSPostingErrMsg = "An error occured posting a payment to NLS.  Loan Number: " + txtLoanNumber.Text + " Reference number: " + sPaymentReference + " Payment Amount: " + sTotalAmount + " Collector Number: " + sNLSUserId;
                            oMail = new RDSSNLSMPUtilsClasses.cEMail();

                            //send payment processing the error so it can be manually posted to NLS.  If email will not work, show the message to the collector
                            try
                            {   
                                oMail.EmailCustomerReceipt(sPaymentPostingEmail, oSettings.NLSPaymentPostingErrorEmail, "NORTRIDGE PAYMENT ERROR (ACH)", strNLSPostingErrMsg);
                                rtxtPaymentResult.Text = "An error occured posting a payment to Nortridge.  Email sent to payment processing.\r\n Loan Number: " + txtLoanNumber.Text + " Reference number: " + sPaymentReference + " Payment Amount: " + sTotalAmount + " Collector Number: " + sNLSUserId;
                            }
                            catch 
                            {
                                string sMsgBody = "Payment not posted to Nortridge.  Email to payment processing failed.  Please notify payment processing with information needed to post payment.\r\n Loan Number: " + txtLoanNumber.Text + " Reference number: " + sPaymentReference + " Payment Amount: " + sTotalAmount + " Collector Number: " + sNLSUserId;
                                rtxtPaymentResult.Text = sMsgBody;
                                return "";
                            }

                        }
                        else
                        {
                            if (nlsRetVal == "True")
                            {
                                rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\n The transaction reference number is: " + sPaymentReference;
                                nls.SaveComment(txtLoanNumber.Text, "A Payment Has Been Made:  Amount: " + sTotalAmount + ", Reference number: " + sPaymentReference + "\r\n Results: " + results,sNLSUserId,"<default>");
                            }
                            else
                            {
                                if (oSettings.QueAllPayemnts=="true") {
                                    rtxtPaymentResult.Text = "Payment Queued for Posting to Nortridge.";
                                    RDSSNLSMPUtilsClasses.cMsgQ oMsgQ = new RDSSNLSMPUtilsClasses.cMsgQ(oSettings.PayQuePath);
                                    oMsgQ.AddMessage(nlsRetVal);
                                }
                                else
                                {
                                    rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\n The transaction reference number is: " + sPaymentReference;
                                    this.Close();
                                }
                            }
                        }


                        //rtxtPaymentResult.Text += results;


                        return rtxtPaymentResult.Text;
                    }
                    return "";
                }
                
        
        
        private void rWizMakeAPayment_Back(object sender, WizardCancelEventArgs e)
        {
            if (this.rWizMakeAPayment.SelectedPage == this.rWizMakeAPayment.Pages[3]) //Check/CC Form
            {
                rbtnOpenPayClient.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            }
        }
        private void rWizMakeAPayment_Next(object sender, WizardCancelEventArgs e)
        {
            RDSSNLSMPUtilsClasses.cNortridgeWapper oNLS = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);
            string PAN ="";

            if (this.rWizMakeAPayment.SelectedPage == this.rWizMakeAPayment.Pages[1]) //Customer Demographics
            {
                

                if (!ValidateCustomerDemoFields())
                {
                    e.Cancel = true;
                }
                rpvPaymethod.SelectedPage = rpvPaymethod.Pages[1];
               // gbxPaymentType.Enabled = true;
               // rbtnCrDebit.CheckState = CheckState.Checked;
                rpvPaymethod.SelectedPage = rpvCCDebit;

                
                //rddlChkSav.Visible = false;
                //rdtpPaymentDate.Visible = false;
                //rlblChkSav.Visible = false;
                //rlblPaymentDate.Visible = false;
                //rlblCCNumber.Enabled = true;
                //rtxtCCNumber.Enabled = true;


                

                //Is ACH enabled or disabled?
             //   bool AchEnabled = oNLS.CheckACHFlag(txtLoanNumber.Text);
            //    if (AchEnabled == false)
            //    {
            //        rlblAlertMessage.ForeColor = System.Drawing.Color.Red;
            //        rlblAlertMessage.Text = "ACH PROCESSING FOR THIS CUSTOMER HAS BEEN DISABLED.  SEE LOAN COMMENTS.";
            //       // rbtnCrDebit.CheckState = CheckState.Checked;

            //        #region Do the credit debit toggle event
            //        //rlblCCNumber.Enabled = true;
            //        //rtxtCCNumber.Enabled = true;


            //        //rlblABANumber.Enabled = false;
            //        //rlblBankAccount.Enabled = false;
            //        //rtxtABANumber.Enabled = false;
            //        //rtxtBankAccount.Enabled = false;


                    
            //        rtxtTotalAmount.Text = "$" ;

            //        //if (this.CurrentState != null)
            //        //    TexasCheck(this.CurrentState);

            //        rtxtCCNumber.Focus();
            //        #endregion

            //    }
            //    //rbtnCheck.Enabled = AchEnabled;

            //    rddlChkSav.SelectedIndex = 0;

            //    rtxtCCNumber.Focus();

            }
            if (this.rWizMakeAPayment.SelectedPage == this.rWizMakeAPayment.Pages[2]) //Check/CC Form
            {
                

                if (!ValidateCCCheckFields())
                {
                    e.Cancel = true;
                    return;
                }

                if (rpvPaymethod.SelectedPage==rpvCheck)
                {
                    PAN = rtxtBankAccount.Text;
                   
                }
                else
                    PAN = rtxtCCNumber.Text;


                rbtnOpenPayClient.Text = "Post Payment";

                rtxtConfirmMsg.Text = "A payment in the amount of " + rtxtTotalAmount.Text + " is about to be applied to loan number " + txtLoanNumber.Text + 
                    " using Card or Bank Account number " + PAN + ".\r\n";
                rtxtConfirmMsg.Text += "\r\n";
                rtxtConfirmMsg.Text += "The payment will be executed on this date:  " + rdtpPaymentDate.Value.ToShortDateString() + "\r\n";
                rtxtConfirmMsg.Text += rtxtPayerName.Text + "\r\n";
                rtxtConfirmMsg.Text += rtxtAddress1.Text + "\r\n";
                rtxtConfirmMsg.Text += rtxtCity.Text + " " + rddlState.Text + " " + rtxtZip.Text;
                rtxtConfirmMsg.Text += "\r\n";
                rtxtConfirmMsg.Text += "\r\n";
                if (rpvPaymethod.SelectedPage==rpvCheck)
                {
                    rtxtConfirmMsg.Text += "Click Post Payment to complete this transaction\r\n";
                }
                else
                {
                    rtxtConfirmMsg.Text += "Click Post Payment to complete this transaction\r\n";
                }
                    
                rtxtConfirmMsg.Text += "or use the Back button to make changes.\r\n";

              

                rbtnOpenPayClient.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                rWizMakeAPayment.FinishButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
                   
                
                oSettings = null;
                oNLS = null;
            }
        }
        void FinishButton_Click(Object sender, EventArgs e)
        {
            
        }
        private void RDSSMakePaymentParent_Load(object sender, EventArgs e)
        {
            RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);
            string CurrentCompany = oSettings.Company;
            int currentYear;


            rdtpPaymentDate.MinDate = DateTime.Today;
            rdtpPaymentDate.MaxDate = DateTime.Today.AddDays(14);

            System.Drawing.Image imglogo = RDDSMakePayments.Properties.Resources.avid_logo;

            rWizMakeAPayment.NextButton.Enabled = false;
            rlblLogonId.Text = "Welcome back: " + this.LogonId; 
            LoadStatesDDL();
            for (int i=0; i<=5; i++){
                currentYear = DateTime.Today.Year + i;

                rddlYear.Items.Add(currentYear.ToString());
            }
        }
        //void TexasCheck(string State)
        //{
        //    RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);
        //    //TEXAS CHECK
        //    if (State.ToUpper() == "TX")
        //    {
        //        rtxtFeeAmt.Text = "0";
        //        rtxtTotalAmount.Text = "0";
        //        rtxtFeeAmt.Enabled = false;
        //        ckbxFees.CheckState = CheckState.Checked;
        //        ckbxFees.Enabled = false;
        //    }
        //    else
        //    {
        //        if (rbtnCheck.CheckState == CheckState.Checked)
        //            rtxtFeeAmt.Text = oSettings.ACHFeeAmount;
        //        else
        //            rtxtFeeAmt.Text = oSettings.CCDebitFeeAmount;

        //        rtxtFeeAmt.Enabled = true;
        //        ckbxFees.CheckState = CheckState.Unchecked;
        //        ckbxFees.Enabled = true;

        //    }
        //}
        void LoadUI(RDSSNLSMPUtilsClasses.cCustomerInfo ci)
        {
            rtxtPayerName.Text = ci.FirstName + " " + ci.LastName;
            rtxtAddress1.Text = ci.Address1.ToString();
            rtxtAddress2.Text = ci.Address2.ToString();
            rtxtCity.Text = ci.City.ToString();
            // rddlState.Enabled = true;
            CurrentState = ci.State.ToString();
            rddlState.SelectedItem = ci.State;
            rtxtZip.Text = ci.ZipCode.ToString();
            rtxtEmail.Text = ci.Email.ToString();
        }
        void LoadUI(RDSSNLSMPUtilsClasses.cLoanInfo li)
        {
            //txtOriginalNote.Text = li.CurrentNoteAmount.ToString();
            //txtCurrDueBalance.Text = li.TotalCurrentDueBalance.ToString();
            //rtxtDaysPastDue.Text = li.DaysPastDue.ToString();
            //txtCurrentBalance.Text = li.CurrentPayOffBalance.ToString();
            //rtxtLoanNumber.Text = li.LoanNumber;
            //rtxtAcctRefNo.Text = li.AcctRefNo;
            //rtxtShortName.Text = li.ShortName;
            //rtxtCurrentDate.Text = li.CurrentDate;
            //rtxtMaturityDate.Text = li.CurrentMaturityDate;
            //rtxtIntAccrThruDate.Text = li.InterestAccruedThruDate;
            //rtxtCurrIntRate.Text = li.CurrentInterestRate;
            //rtxtNextBillingDte.Text = li.NextBillingDate;
            //rtxtLastPayDate.Text = li.LastPaymentDate;
            //rtxtLastPaymentAmount.Text = li.LastPaymentAmount;
            rtxtCustomerName.Text = li.AccountName;


            rlblCurrentDue.Text = "Current Due: $" + li.TotalCurrentDueBalance.ToString();

            string PaymentAmt = rspnPayDollars.Value + "." + rspnPayCents.Value;

            rtxtTotalAmount.Text = double.Parse(PaymentAmt).ToString();
        }

        //void LoadUI(List<cPaymentHistory> phlist)
        //{


        //    foreach (cPaymentHistory ph in phlist)
        //    {
        //        if (ph.AcctRefNo != null)
        //        {
        //            string[] PaymentHistory = new string[] {ph.AcctRefNo.ToString(),ph.PaymentNumber .ToString(),ph.PaymentAmount.ToString(),
        //        ph.PaymentDescription.ToString(),ph.PaymentMethodCode.ToString(),ph.PaymentMethodReference.ToString(),
        //        ph.ACHTraceNumber.ToString(),ph.DateDue.ToString(),ph.DatePaid.ToString(),
        //        ph.NSFDate.ToString(),ph.NSFFlag};




        //            rgvPaymentHistory.Rows.Add(PaymentHistory);
        //            rgvPaymentHistory.Refresh();
        //        }
        //    }

        //}


        private void btnFindLoan_Click(object sender, EventArgs e)
        {
            string FindCustomerVal = "";
             try
            {
                FEUtils feu = new FEUtils();
                RDSSNLSMPUtilsClasses.cCustomerInfo ci = new RDSSNLSMPUtilsClasses.cCustomerInfo();

                string CustomerNumber = "";
                
                RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();

                FindCustomerVal=feu.FindLoan(txtLoanNumber.Text);
                if (FindCustomerVal.IndexOf("Not Found") > 0 || FindCustomerVal.IndexOf("Error") > 0)
                {
                    //show error or message but do not allow the wizard to continue
                    rtxtCustomerName.Text = FindCustomerVal;
                    rWizMakeAPayment.NextButton.Enabled = false;

                    LoanLoaded = false;
                }
                else
                {
                    //show error or message but do not allow the wizard to continue
                   

                    CustomerNumber = nls.GetCustomerNumberByLoan(txtLoanNumber.Text);

                    ci = nls.GetCustomerInformation(CustomerNumber);
                    LoadUI(ci);

                    rtxtCustomerName.Text = FindCustomerVal + "\r\n";
                    rtxtCustomerName.Text += ci.Address1.ToString() + "\r\n";
                    rtxtCustomerName.Text += ci.City.ToString() + ", " + ci.State + " " + ci.ZipCode;

                    rWizMakeAPayment.NextButton.Enabled = true;
                    LoanLoaded = true;
                }
                

            }
            catch (Exception ex)
            {
                rtxtCustomerName.Text=ex.Message;
                LoanLoaded = false;
            }
        }
        
        

        private void rbtnCrDebit_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);

            rpvPaymethod.SelectedPage = rpvCCDebit;
            


            rtxtABANumber.Text = "";
            rtxtBankAccount.Text = "";

            

            //if (this.CurrentState != null)
            //    TexasCheck(this.CurrentState);

            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadStatesDDL()
        {
            string States = "AK,AL,AR,AZ,CA,CO,CT,DC,DE,FL,GA,HI,IA,ID,IL,IN,KS,KY,LA,MA,MD,ME,MI,MN,MO,MS,MT,NC,ND,NE,NH,NJ,NM,NV,NY,OH,OK,OR,PA,RI,SC,SD,TN,TX,UT,VA,VT,WA,WI,WV,WY";
            string[] ArrayOfStates = States.Split(',');
            int iCntr = 0;

            foreach (string st in ArrayOfStates)
            {


                rddlState.Items.Insert(iCntr, st);
                iCntr++;

            }


        }

        private void rbtnCheck_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);

           
            rpvPaymethod.SelectedPage = rpvCheck;

            

            //rtxtABANumber.Enabled = true;
            //rlblABANumber.Enabled = true;
            //rtxtBankAccount.Enabled = true;
            //rlblBankAccount.Enabled = true;

           

            rtxtCCNumber.Text = "";
            //rtxtCVV.Text = "";


          

            //if (this.CurrentState != null)
            //    TexasCheck(this.CurrentState);

            rdtpPaymentDate.MinDate = DateTime.Today;
            rdtpPaymentDate.MaxDate = DateTime.Today.AddDays(14);
            rlblPaymentDate.Visible = true;
            rdtpPaymentDate.Visible = true;

           
            oSettings = null;
        }

        private void rbtnCalcTotal_Click(object sender, EventArgs e)
        {
            //figure out how to do this without having to hit a button...
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);
            try
            {
                decimal PaymentAmt_Dollars = rspnPayDollars.Value;
                decimal PaymentAmt_Cents = rspnPayCents.Value / 100;
                decimal PaymentAmt_Fees = 0, PaymentAmt=0;


               
                    PaymentAmt = PaymentAmt_Dollars + PaymentAmt_Cents + 0;
            

                rtxtTotalAmount.Text = PaymentAmt.ToString("C");
                rWizMakeAPayment.NextButton.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                rWizMakeAPayment.NextButton.Enabled = false;
            }
        }

        

       


        void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                try
                {
                    FEUtils feu = new FEUtils();
                    feu.FindLoan(txtLoanNumber.Text);
                    LoanLoaded = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }


        private void rbtnOpenPayClient_Click(object sender, EventArgs e)
        {

            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);
           
            string ACHType =oSettings.ACHType;
            string sRetVal = "";

            rtxtPaymentResult.Text = "";

            rWizMakeAPayment.BackButton.Visibility = Telerik.WinControls.ElementVisibility.Visible;

            if (rpvPaymethod.SelectedPage==rpvCheck)
            {
                if (ACHType == "ECHECK") //if date to execute is greater than today then create pending trans
                {
                    if (rdtpPaymentDate.Value == DateTime.Today)
                    {
                        sRetVal = this.MakePaymentCheck();
                    }
                    if (rdtpPaymentDate.Value > DateTime.Today)
                    {
                        sRetVal = this.MakePaymentCheck(true);
                    }
                }
                if (ACHType == "NACHA")
                {
                    sRetVal = this.MakePaymentACHNacha();
                }


                //if there is an error then set the enabled and visibility settings to allow clerk to make changes.
                if (sRetVal.IndexOf("ERROR") > -1)
                {
                    rWizMakeAPayment.BackButton.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    rbtnOpenPayClient.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    rWizMakeAPayment.CancelButton.Text = "Cancel";
                    return;
                }
                else
                {
                    rWizMakeAPayment.BackButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
                    rbtnOpenPayClient.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
                    rWizMakeAPayment.CancelButton.Text = "Close";
                    return;
                }

                //else
                //{
                //   sRetVal =  this.MakePaymentCheck();
                //}
            }

            if (rpvPaymethod.SelectedPage==rpvCCDebit)
            {
                if (rdtpPaymentDate.Value == DateTime.Today)
                {
                    sRetVal = this.MakePaymentCredit();
                }
                if (rdtpPaymentDate.Value > DateTime.Today)
                {
                    sRetVal = this.MakePaymentCredit(true);
                }


                //if there is an error then set the enabled and visibility settings to allow clerk to make changes.
                if (sRetVal.IndexOf("ERROR") > -1)
                {
                    rWizMakeAPayment.BackButton.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    rWizMakeAPayment.CancelButton.Text = "Cancel";
                    return;
                }
                if (sRetVal.IndexOf("FINISH") > -1)
                {
                    rWizMakeAPayment.BackButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
                    rbtnOpenPayClient.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
                    rWizMakeAPayment.CancelButton.Text = "Close";
                    return;
                }
                
                if (sRetVal.IndexOf("DECLINED") > -1)
                {
                    rWizMakeAPayment.BackButton.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    rWizMakeAPayment.CancelButton.Text = "Cancel";
                    return;
                }
            }

        }

         

        private void lnklblClear_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rtxtPayerName.Text = "";
            rtxtAddress1.Text = "";
            rtxtCity.Text = "";
            rddlState.Text = "";
            rtxtZip.Text = "";
            rtxtPayerName.Focus();
        }

        private void rtxtABANumber_Leave(object sender, EventArgs e)
        {

            //RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);

            //System.Drawing.Image imggreen = RDDSMakePayments.Properties.Resources.blank_green_icon;
            //System.Drawing.Image imgyellow = RDDSMakePayments.Properties.Resources.blank_yellow_icon;

            //if (oSettings.ValidateRoutingNumber == "true")
            //{
            //    RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            //    RDSSNLSMPUtilsClasses.cSettings osettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);
               
                
            //}
        }

        private void rtxtABANumber_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void rtxtABANumber_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void pnlCLN_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picbxLogo_Click(object sender, EventArgs e)
        {

        }

        private void pnlConfirm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rspnPayDollars_ValueChanged(object sender, EventArgs e)
        {
            rbtnCalcTotal_Click(sender, e);
        }

        private void rspnPayCents_ValueChanged(object sender, EventArgs e)
        {
            rbtnCalcTotal_Click(sender, e);
        }

        private void rpnlCCACH_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rspnPayDollars_ValueChanged_1(object sender, EventArgs e)
        {
            rbtnCalcTotal_Click(sender,e);
        }

        private void rspnPayCents_ValueChanged_1(object sender, EventArgs e)
        {
            rbtnCalcTotal_Click(sender, e);
        }
       

        
    }
     
}
