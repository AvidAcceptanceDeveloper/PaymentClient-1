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
using System.Deployment.Application;


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

            rtxtDollars.KeyPress += new KeyPressEventHandler(rtxtDollars_KeyPress);
            rtxtCents.KeyPress += new KeyPressEventHandler(rtxtCents_KeyPress);


            if( System.Diagnostics.Debugger.IsAttached) 
                lblVersion.Text = "Debug Mode";
            else{
                ApplicationIdentity ai = new ApplicationIdentity("RDSS - Payment Client (Avid)");
                lblVersion.Text = "Current Application Version: " + ApplicationDeployment.CurrentDeployment.CurrentVersion.Major.ToString() + "." + ApplicationDeployment.CurrentDeployment.CurrentVersion.Minor.ToString() + "." + ApplicationDeployment.CurrentDeployment.CurrentVersion.Revision.ToString();
            }
                

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
            string sTotalAmount = rtxtDollars.Text + "." + rtxtCents.Text;

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
                if (sTotalAmount == ".")
                {
                    lblAmount.ForeColor = System.Drawing.Color.Red;
                    rtxtDollars.Focus();
                    return false;
                }
                else
                {
                    lblAmount.ForeColor = System.Drawing.Color.Black;
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
            if (sTotalAmount == ".")
            {
                lblAmount.ForeColor = System.Drawing.Color.Red;

                rtxtDollars.Focus();
                return false;
            }
            else
            {
                lblAmount.ForeColor = System.Drawing.Color.Black;
            }
            return true;
        }
        private void rWizMakeAPayment_Cancel(object sender, System.EventArgs e)
        {
            if (rWizMakeAPayment.CancelButton.Text == "Cancel")
            {
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("Do you wish to Cancel?", "Cancel Current Payment", MessageBoxButtons.YesNo);
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
            //long i = 1;
            //foreach (byte b in Guid.NewGuid().ToByteArray())
            //{
            //    i *= ((int)b + 1);
            //
            string sId = "";
            StringBuilder sb = new StringBuilder();
            sId = sb.Append(DateTime.Now.Month.ToString()).Append(DateTime.Now.Day.ToString()).Append(DateTime.Now.Year.ToString()).Append(DateTime.Now.Millisecond.ToString()).ToString();
            return sId;
        }
        private string MakePaymentCredit(bool bCreateSubscription=false)
        {
            RDSSNLSMPUtilsClasses.cEMail oMail = new RDSSNLSMPUtilsClasses.cEMail();
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile.ToString());
           

            // string MPFirstMileSettings = oSettings.MPSettings;
            string sTotalAmount = "";
            string sTotalFeeAmount = "", sMerchantReceipt = "", sCustomerDemoInfo = "";
            string sPaymentPostingEmail = "";
            string strNLSPostingErrMsg = "";
            string sNLSUserId = "";
            
            string sPaymentReference = "";
            bool IncludeFees = false;
            string[] StatusArray=null;
            string ExpireDte = "";
            string sBody = "";

            RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            
            RDSSNLSMPUtilsClasses.cPaymentTech oPayTech = new RDSSNLSMPUtilsClasses.cPaymentTech();

          

            StringBuilder sb = new StringBuilder();
            StringBuilder sb_merchant = new StringBuilder();
            StringBuilder sb_customerdemo = new StringBuilder();

            string LegalEntity = nls.GetLoanGroupByLoan(txtLoanNumber.Text);


            string[] NameSplit = rtxtPayerName.Text.Split(' ');
            string sFName = NameSplit[NameSplit.GetLowerBound(0)].ToString();
            string sLName = NameSplit[NameSplit.GetUpperBound(0)].ToString();
            var results = "";

            IncludeFees = true;
           
           
            ExpireDte =   rddlYear.SelectedItem.ToString() + rddlMonth.SelectedItem.ToString();
            sTotalAmount = rtxtDollars.Text + rtxtCents.Text;

            sPaymentPostingEmail = rtxtEmail.Text;

            string _address1="", _address2 = "";

            //described to me by integration support, if there is data in address2 send it through address1 and address1 through address2.
            if (rtxtAddress2.TextLength > 0)
            {
                _address1 = rtxtAddress2.Text;
                _address2 = rtxtAddress1.Text;

            }
            else {
                _address1 = rtxtAddress1.Text;
                _address2 = "";
            }

            if (bCreateSubscription == true)
            {

                results = oPayTech.CCMarkForCaptureByProfile(txtLoanNumber.Text + GenerateId(), sTotalAmount, rtxtCCNumber.Text, ExpireDte, "", rtxtPayerName.Text, rtxtAddress1.Text, rtxtCity.Text, rddlState.SelectedItem.ToString(), rtxtZip.Text, _address2, rdtpPaymentDate.Value.ToShortDateString());
            }
            else
            {
                results = oPayTech.CCMarkForCapture(txtLoanNumber.Text + GenerateId(), sTotalAmount, rtxtCCNumber.Text ,ExpireDte,rtxtSecCode.Text, rtxtPayerName.Text, _address1, rtxtCity.Text, rddlState.SelectedItem.ToString(), rtxtZip.Text,_address2);
            }

            if (results.ToUpper().IndexOf("ERROR") == 0)  //leave form state alone
            {


                if (bCreateSubscription)
                    rtxtPaymentResult.Text = results;
                else {
                    StatusArray = results.Split('|');
                    rtxtPaymentResult.Text = StatusArray[0] + "\r\nReference: " + StatusArray[1];
                }

                return "DECLINED";
            }
            else
            {

                if (bCreateSubscription)
                {
                    sPaymentReference = results;  //created a profile, user the reference # for payment reference
                    
                }
                else
                {
                    StatusArray = results.Split('|');
                    sPaymentReference = StatusArray[0].ToString() + StatusArray[3].ToString();
                }
                IncludeFees = false;  //for Avid, no fees with transactions

                  

                    
                    //add back decimal point
                    sTotalAmount =  (Double.Parse(sTotalAmount) * .01).ToString();
                
                string nlsRetVal = nls.PayByCCDebit(txtLoanNumber.Text, "", sTotalAmount, sTotalFeeAmount,rdtpPaymentDate.Value.ToShortDateString() , sPaymentReference, this.NLSUserId, IncludeFees.ToString(), results);
                
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
                        oMail.EmailCustomerReceipt(oSettings.NLSPaymentPostingErrorEmail, oSettings.CustomerServiceEmail, "NORTRIDGE PAYMENT ERROR (ACH)", strNLSPostingErrMsg);
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
                        if (bCreateSubscription)
                        {
                            //rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\nPayment Reference: " + results;
                            rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\nPayment Reference: " + sPaymentReference;
                            if (sPaymentPostingEmail.Length > 0 && sPaymentPostingEmail.IndexOf("@") >= 0)
                            {

                                sBody = "Thank you for calling Avid Acceptance.\r\n" +
                                        "Your payment transaction has been scheduled and will execute on: " + rdtpPaymentDate.Text + ". \r\n" +
                                        "Loan #: " + txtLoanNumber.Text + "\r\n" +
                                        "Total: " + sTotalAmount + "\r\n" +
                                        "Please Allow 24 hours after scheduled date for your account to update.";

                                oMail.EmailCustomerReceipt(sPaymentPostingEmail, oSettings.CustomerServiceEmail, "Payment  (CCDEBIT)  Reference: " + sPaymentReference, sBody);

                            }
                        }
                        else {
                            rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\nPayment Reference: " + sPaymentReference;
                            if (sPaymentPostingEmail.Length > 0 && sPaymentPostingEmail.IndexOf("@") >= 0)
                            {
                                sBody = "Thank you for calling Avid Acceptance.\r\n" +
                                        "Your payment transaction has been scheduled and will execute on: " + rdtpPaymentDate.Text + ". \r\n" +
                                        "Loan #: " + txtLoanNumber.Text + "\r\n" +
                                        "Total: " + sTotalAmount + "\r\n" +
                                        "Please Allow 24 hours after scheduled date for your account to update.";

                                oMail.EmailCustomerReceipt(sPaymentPostingEmail, oSettings.CustomerServiceEmail, "Payment  (CCDEBIT)  Reference: " + sPaymentReference, sBody);
                                
                            }
                        }

                    }
                    else
                    {

                        if (bCreateSubscription)
                        {
                            rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\n" + results; //authocode
                           // nls.SaveComment(txtLoanNumber.Text, "A Payment Has Been Made:  Amount: $" + sTotalAmount + ", Order Id: " + sPaymentReference + "\r\n AuthCode: " + results + " Reference: " + results, sNLSUserId, "<default>");
                        }
                        else {
                            rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\n" + StatusArray[3]; //authocode
                           // nls.SaveComment(txtLoanNumber.Text, "A Payment Has Been Made:  Amount: $" + sTotalAmount + ", Order Id: " + sPaymentReference + "\r\n AuthCode: " + StatusArray[3] + " Reference: " + StatusArray[2], sNLSUserId, "<default>");
                        }
                        this.Close();
                        
                    }
                }


                //rtxtPaymentResult.Text += "\r\nOrder Id:  " + sPaymentReference + "\r\nOrbital Reference: " + StatusArray[2];
                
               
            }
            return "FINISH";
           
        }
        private string MakePaymentACHNacha()
        {
            RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            string CheckingOrSavings = "";
            string sTotalAmount = rtxtDollars.Text + rtxtCents.Text;

            if (rddlChkSav.SelectedItem.Text  == "Checking")
            {
                CheckingOrSavings = "0";
            }
            else
            {
                CheckingOrSavings = "1";
            }
            string nlsRetVal = nls.PayByACHNacha(txtLoanNumber.Text, "1", rtxtABANumber.Text, rtxtBankAccount.Text, sTotalAmount, "0", "NACHA FILE", NLSUserId, "false", "", rdtpPaymentDate.Value, rtxtEmail.Text, CheckingOrSavings);
            string[] sPaymentReference = nlsRetVal.Split('|');

            if (sPaymentReference.GetUpperBound(0) > 0)
            {
                if (sPaymentReference[0] == "True")
                {

                    rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\n";
                    rtxtPaymentResult.Text += "Amount: " + sTotalAmount + "\r\n";
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
           
            RDSSNLSMPUtilsClasses.cEMail oMail = new RDSSNLSMPUtilsClasses.cEMail();
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile.ToString());

            string[] NameSplit = rtxtPayerName.Text.Split(' ');
            string sFName = NameSplit[NameSplit.GetLowerBound(0)].ToString();
            string sLName = NameSplit[NameSplit.GetUpperBound(0)].ToString();
           
            string sTotalAmount = "", sRetry="";
            string sTotalFeeAmount = "", sMerchantReceipt = "", sCustomerDemoInfo="";
            string sPaymentPostingEmail = "";
            string strNLSPostingErrMsg = "";
            string sNLSUserId = "";
            string sBody = "";
           
            string sPaymentReference = "";

            string nlsRetVal = "";
            bool IncludeFees = false;

            string[] StatusArray=null;
            
            RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            //RDSSNLSMPUtilsClasses.cAuthNet oAuthNet = new RDSSNLSMPUtilsClasses.cAuthNet();
            RDSSNLSMPUtilsClasses.cPaymentTech oPayTech = new RDSSNLSMPUtilsClasses.cPaymentTech();

            StringBuilder sb = new StringBuilder();
            StringBuilder sb_merchant = new StringBuilder();
            StringBuilder sb_customerdemo = new StringBuilder();

            string LegalEntity = nls.GetLoanGroupByLoan(txtLoanNumber.Text);
            string results = "";

            IncludeFees = false;



            sTotalAmount = rtxtDollars.Text + rtxtCents.Text;
            sPaymentPostingEmail = rtxtEmail.Text;

            ////strip comma from numbers larger than 999.99
            //sTotalAmount = Double.Parse(sTotalAmount).ToString();

            string CheckingOrSavings = "";

            if (rddlChkSav.SelectedItem.Text == "Checking")
            {
                CheckingOrSavings = "C";
            }
            else
            {
                CheckingOrSavings = "S";
            }

            string _address1 = "", _address2 = "";

            //described to me by integration support, if there is data in address2 send it through address1 and address1 through address2.
            if (rtxtAddress2.TextLength > 0)
            {
                _address1 = rtxtAddress2.Text;
                _address2 = rtxtAddress1.Text;

            }
            else {
                _address1 = rtxtAddress1.Text;
                _address2 = "";
            }


             


            if (bCreateSubscription==true)
            {

                
                results = oPayTech.ECMarkForCaptureByProfile(txtLoanNumber.Text + GenerateId(), sTotalAmount, rtxtABANumber.Text, rtxtBankAccount.Text, CheckingOrSavings, rdtpPaymentDate.Value.ToShortDateString(), rtxtPayerName.Text, _address1, rtxtCity.Text, rddlState.SelectedItem.ToString(), rtxtZip.Text, _address2);

               

                if (results != "ERROR")
                {
                    sTotalAmount = (Double.Parse(sTotalAmount) * .01).ToString();  // put decimal back into total amount
                    nlsRetVal = nls.PayByCheck(txtLoanNumber.Text, "", "", sTotalAmount, sTotalFeeAmount, results, rdtpPaymentDate.Value.ToString(), NLSUserId, IncludeFees.ToString(), results);
                }
            }                                        
            else{
                results = oPayTech.ECMarkForCapture(txtLoanNumber.Text + GenerateId(), sTotalAmount, rtxtABANumber.Text, rtxtBankAccount.Text, CheckingOrSavings,"",rtxtPayerName.Text,_address1 ,rtxtCity.Text ,rddlState.SelectedItem.ToString(),rtxtZip.Text, _address2  );
            }   

                    
                    if (results.ToUpper().IndexOf("ERROR") == 0)  //leave form state alone
                    {
                        rtxtPaymentResult.Text = results;
                        return "ERROR";
                    }
                    if (results.ToUpper().IndexOf("APPROVED") >=0) //post to NLS and close form
                    {
                        if (bCreateSubscription)
                        {

                            sPaymentReference = results;
                        }
                        else {
                            StatusArray = results.Split('|');
                            sPaymentReference = StatusArray[0].ToString() + StatusArray[3].ToString();
                        }

                        rWizMakeAPayment.BackButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
                        rWizMakeAPayment.CancelButton.Text = "Close";

                sTotalAmount = (Double.Parse(sTotalAmount) * .01).ToString();  // put decimal back into total amount
                nlsRetVal = nls.PayByCheck(txtLoanNumber.Text, "", "", sTotalAmount, sTotalFeeAmount, sPaymentReference,rdtpPaymentDate.Value.ToString(), NLSUserId, IncludeFees.ToString(), results);
                            
                            
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
                                oMail.EmailCustomerReceipt(oSettings.NLSPaymentPostingErrorEmail, oSettings.NLSPaymentPostingErrorEmail, "NORTRIDGE PAYMENT ERROR (ACH)", strNLSPostingErrMsg);
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

                    if (bCreateSubscription)
                    {
                        if (sPaymentPostingEmail.Length > 0 && sPaymentPostingEmail.IndexOf("@") >= 0)
                        {

                            rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\n The transaction reference number is: " + results;
                            nls.SaveComment(txtLoanNumber.Text, "A Payment Has Been Made:  Amount: $" + sTotalAmount + ",  Reference: " + results, sNLSUserId, "<default>");

                            sBody = "Thank you for calling Avid Acceptance.\r\n" +
                           "Your payment transaction has been scheduled and will execute on: " + rdtpPaymentDate.Text + ". \r\n" +
                           "Loan #: " + txtLoanNumber.Text + "\r\n" +
                           "Total: " + sTotalAmount + "\r\n" +
                           "Please Allow 24 hours after scheduled date for your account to update.";

                           //"<img src=\'cid:companyLogo\' width='104' height='27' /></p>";
                            oMail.EmailCustomerReceipt(sPaymentPostingEmail, oSettings.CustomerServiceEmail, "Payment  (ECP)  Reference: " + results, sBody);
                        }
                    }
                    else {
                        if (sPaymentPostingEmail.Length > 0 && sPaymentPostingEmail.IndexOf("@") >= 0)
                        {
                            rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\n The transaction reference number is: " + sPaymentReference;
                            nls.SaveComment(txtLoanNumber.Text, "A Payment Has Been Made:  Amount: $" + sTotalAmount + ", Order Id: " + sPaymentReference + "\r\n AuthCode: " + StatusArray[3] + " Reference: " + StatusArray[2], sNLSUserId, "<default>");

                            sBody = "Thank you for calling Avid Acceptance.\r\n" +
                                    "Your payment transaction has been scheduled and will execute on: " + rdtpPaymentDate.Text + ". \r\n" +
                                    "Loan #: " + txtLoanNumber.Text + "\r\n" +
                                    "Total: " + sTotalAmount + "\r\n" +
                                    "Please Allow 24 hours after scheduled date for your account to update.";

                            oMail.EmailCustomerReceipt(sPaymentPostingEmail, oSettings.CustomerServiceEmail, "Payment  (ECP) Reference: " + sPaymentReference, sBody);
                        }
                    }
                            }
                            else
                            {
                               
                                    rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\n The transaction reference number is: " + sPaymentReference;
                                    //this.Close();
                               
                            }
                        }


                        //rtxtPaymentResult.Text += results;


                        return rtxtPaymentResult.Text;
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
            StringBuilder sb = new StringBuilder();

            string PAN ="";

            if (this.rWizMakeAPayment.SelectedPage == this.rWizMakeAPayment.Pages[1]) //Customer Demographics
            {
                

                if (!ValidateCustomerDemoFields())
                {
                    e.Cancel = true;
                }
                rpvPaymethod.SelectedPage = rpvPaymethod.Pages[1];
                rpvPaymethod.SelectedPage = rpvCCDebit;
                rtxtCCNumber.Focus();


            }
            if (this.rWizMakeAPayment.SelectedPage == this.rWizMakeAPayment.Pages[2]) //Check/CC Form
            {

                string sTotalAmount="";
                rtxtPaymentResult.Text = "";

                if (rtxtCents.Text == "") { rtxtCents.Text = "00"; }

                sTotalAmount = rtxtDollars.Text + rtxtCents.Text;
                rbtnOpenPayClient.Text = "Post Payment";

                if (!ValidateCCCheckFields())
                {
                    e.Cancel = true;
                    return;
                }

                if (rpvPaymethod.SelectedPage == rpvCheck)
                {
                    PAN = rtxtBankAccount.Text;
                    rtxtBankAccount.Focus();

                    rtxtConfirmMsg.Text = sb.Append(rtxtPayerName.Text)
                    .Append(", BY PROVIDING US WITH YOUR BANK ACCOUNT INFORMATION AND VERBAL AUTHORIZATION\r\n\r\nTODAY, ")
                    .Append(String.Format("{0:MM / dd / yyyy}", DateTime.Now))
                    .Append(", YOU ARE AUTHORIZING AVID ACCEPTANCE TO WITHDRAW THE AMOUNT OF $")
                    .Append(String.Format("{0:C}", rtxtDollars.Text + "." + rtxtCents.Text))
                    .Append(" AS A ONE-TIME ACH DEBIT. ")
                    .Append("\r\n")
                    .Append("\r\n")
                    .Append("THE PAYMENT WILL BE POSTED TO YOUR AVID ACCOUNT ON ")
                    .Append(rdtpPaymentDate.Value.ToShortDateString()).Append("\r\n\r\n")
                    .Append(" PLEASE ALLOW 1 - 3 BUSINESS DAYS FOR THIS PAYMENT TO BE WITHDRAWN FROM YOUR BANK ACCOUNT. ")
                    .Append("\r\n")
                    .Append("\r\n")
                    .Append("DO YOU AUTHORIZE AVID ACCEPTANCE TO PROCEED WITH THIS PAYMENT TODAY?\r\n")
                    .Append("\r\n")
                    .Append(" IF YOU HAVE ANY QUESTIONS REGARDING THIS PAYMENT OR WISH TO REVOKE THE PAYMENT AUTHORIZATION WITHIN ONE HOUR, ")
                    .Append("YOU MAY REACH US AT 1 - 888 - 777 - 9190.").ToString();


                }
                else
                {
                    PAN = rtxtCCNumber.Text;
                    rtxtCCNumber.Focus();
                    rtxtConfirmMsg.Text = sb.Append(rtxtPayerName.Text).Append(", BY PROVIDING US WITH YOUR DEBIT CARD INFORMATION AND VERBAL AUTHORIZATION TODAY, ")
                    .Append("\r\n")
                    .Append("\r\n")
                    .Append(String.Format("{0:MM / dd / yyyy}", DateTime.Now))
                    .Append(" YOU ARE AUTHORIZING AVID ACCEPTANCE TO WITHDRAW THE AMOUNT OF $")
                    .Append(String.Format("{0:C}", rtxtDollars.Text + "." + rtxtCents.Text))
                    .Append(" AS A ONE-TIME DEBIT. ")
                    .Append("\r\n")
                    .Append("\r\n")
                    .Append("THE PAYMENT WILL BE POSTED TO YOUR AVID ACCOUNT ON ")
                    .Append(rdtpPaymentDate.Value.ToShortDateString())
                    .Append("\r\n")
                    .Append("\r\n")
                    .Append(" DO YOU AUTHORIZE AVID ACCEPTANCE TO PROCEED WITH THIS PAYMENT TODAY?")
                    .Append("\r\n")
                    .Append("\r\n")
                    .Append(" IF YOU HAVE ANY QUESTIONS REGARDING THIS PAYMENT OR WISH TO REVOKE THE PAYMENT AUTHORIZATION WITHIN ")
                    .Append("\r\n")
                    .Append("\r\n")
                    .Append("ONE HOUR, YOU MAY REACH US AT 1 - 888 - 777 - 9190.").ToString();

                   

                }



                //rtxtConfirmMsg.Text = "A payment in the amount of $" + rtxtDollars.Text + "." + rtxtCents.Text + " is about to be applied to loan number " + txtLoanNumber.Text + 
                //    " using Card or Bank Account number " + PAN + ".\r\n";
                //rtxtConfirmMsg.Text += "\r\n";
                //rtxtConfirmMsg.Text += "The payment will be executed on this date:  " + rdtpPaymentDate.Value.ToShortDateString() + "\r\n";
                //rtxtConfirmMsg.Text += "\r\n";
                //rtxtConfirmMsg.Text += rtxtPayerName.Text + "\r\n";
                //rtxtConfirmMsg.Text += rtxtAddress1.Text + "\r\n";
                //rtxtConfirmMsg.Text += rtxtCity.Text + " " + rddlState.Text + " " + rtxtZip.Text;
                //rtxtConfirmMsg.Text += "\r\n";
                //rtxtConfirmMsg.Text += "\r\n";












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

            for (int i=0; i<=10; i++){
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
            //rtxtPayerName.Text = ci.FirstName + " " + ci.LastName;
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
            //rtxtCustomerName.Text = li.AccountName;


            //rlblCurrentDue.Text = "Current Due: $" + li.TotalCurrentDueBalance.ToString();

            //string PaymentAmt = rtxtTotalAmount.Text;

            //rtxtTotalAmount.Text = double.Parse(PaymentAmt).ToString();
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
                    rtxtPayerName.Text = FindCustomerVal;
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

        //private void rbtnCalcTotal_Click(object sender, EventArgs e)
        //{
        //    //figure out how to do this without having to hit a button...
        //    RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);
        //    try
        //    {
        //        //decimal PaymentAmt_Dollars = rspnPayDollars.Value;
        //        //decimal PaymentAmt_Cents = rspnPayCents.Value / 100;
        //        decimal PaymentAmt_Fees = 0, PaymentAmt=0;



        //        PaymentAmt = rtxtTotalAmount.Text; ;
            

        //        rtxtTotalAmount.Text = PaymentAmt.ToString("C");
        //        rWizMakeAPayment.NextButton.Enabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        rWizMakeAPayment.NextButton.Enabled = false;
        //    }
        //}

        

       


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
            DateTime TransactionDate = rdtpPaymentDate.Value;

            rtxtPaymentResult.Text = "";

            rbtnOpenPayClient.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            rWizMakeAPayment.BackButton.Visibility = Telerik.WinControls.ElementVisibility.Visible;

            if (rpvPaymethod.SelectedPage==rpvCheck)
            {
                if (ACHType == "ECHECK") //if date to execute is greater than today then create pending trans
                {
                    if (TransactionDate == DateTime.Today)
                    {
                        sRetVal = this.MakePaymentCheck();
                    }
                    if (TransactionDate > DateTime.Today)
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

                
            }

            if (rpvPaymethod.SelectedPage==rpvCCDebit)
            {
                if (TransactionDate == DateTime.Today)
                {
                    sRetVal = this.MakePaymentCredit();
                }
                if (TransactionDate > DateTime.Today)
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

        //private void rspnPayDollars_ValueChanged(object sender, EventArgs e)
        //{
        //    rbtnCalcTotal_Click(sender, e);
        //}

        //private void rspnPayCents_ValueChanged(object sender, EventArgs e)
        //{
        //    rbtnCalcTotal_Click(sender, e);
        //}

        private void rpnlCCACH_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rpvCCDebit_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void rtxtDollars_TextChanged(object sender, EventArgs e)
        {

        }

        private void rtxtDollars_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void rtxtDollars_KeyPress(object sender, KeyPressEventArgs e)
        {
            int x;
            if (int.TryParse(e.KeyChar.ToString(), out x))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                if (e.KeyChar.ToString() == ".")
                {
                    rtxtCents.Focus();
                }
            }
        }
        private void rtxtCents_KeyPress(object sender, KeyPressEventArgs e)
        {
            int x;
            if (int.TryParse(e.KeyChar.ToString(), out x))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void rbtnTestEmail_Click(object sender, EventArgs e)
        {
            RDSSNLSMPUtilsClasses.cEMail oemail = new RDSSNLSMPUtilsClasses.cEMail();
            string sBody = "Thank you for calling Avid Acceptance.\r\n" +
                            "Your payment transaction has processed:\r\n" +
                            "Loan #: 34834888 \r\n" +
                            "Total: $350.00\r\n" +
                            "Please Allow 24 hours upon receipt of this confirmation for your account to update.\r\n";
                            //"<img src=\'cid:companyLogo\' width='104' height='27' /></p>";
            oemail.EmailCustomerReceipt("hilltx@gmail.com", "PaymentConfirmation@AvidAcceptance.com", "Avid Payment Confirmation", sBody);
        }

        private void lblVersion_Click(object sender, EventArgs e)
        {

        }




        //private void rspnPayDollars_ValueChanged_1(object sender, EventArgs e)
        //{
        //    rbtnCalcTotal_Click(sender,e);
        //}

        //private void rspnPayCents_ValueChanged_1(object sender, EventArgs e)
        //{
        //    rbtnCalcTotal_Click(sender, e);
        //}



    }
     
}
