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

        public RDSSMakePaymentParent()
        {
            InitializeComponent();

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
           
            if (rbtnCrDebit.CheckState == CheckState.Checked)
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
                
            }
            if (rbtnCheck.CheckState == CheckState.Checked)
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
        private string MakePaymentCredit()
        {
            RDSSNLSMPUtilsClasses.cEMail oMail = null;
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile.ToString());

            string MPFirstMileSettings = oSettings.MPSettings;
            string sTotalAmount = "";
            string sTotalFeeAmount = "", sMerchantReceipt = "", sCustomerDemoInfo = "";
            
            string sPaymentPostingEmail = "";
            string strNLSPostingErrMsg = "";
            string sNLSUserId = "";
            string results = "";
            //string sAccountId = "", sMerchantPIN = "", sMerchantReceipt = "";
            //string sHideStuff = "", sCustomerDemoInfo = "", 
            string sPaymentReference = "";

            //bool bCCDebit = false;
            bool IncludeFees = false;

            string[] StatusArray;

            RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            ATSSecurePostUILib.ATSSecurePostUI PostUI = null;

            StringBuilder sb = new StringBuilder();
            StringBuilder sb_merchant = new StringBuilder();
            //StringBuilder sb_hidestuff = new StringBuilder();
            StringBuilder sb_customerdemo = new StringBuilder();

            string LegalEntity = nls.GetLoanGroupByLoan(txtLoanNumber.Text);
            if (ckbxFees.Checked)
            {
                IncludeFees = false;
            }
            else
            {
                IncludeFees = true;
            }

            PostUI = new ATSSecurePostUILib.ATSSecurePostUI(); //Merchant Partners First Mile Assembly

            sTotalAmount = rtxtTotalAmount.Text.Substring(1, rtxtTotalAmount.Text.Length - 1);
            sTotalFeeAmount = rtxtFeeAmt.Text;

            sTotalAmount = Double.Parse(sTotalAmount).ToString();
            

            sCustomerDemoInfo = sb_customerdemo.Append(" /Address1:" + rtxtAddress1.Text).Append(" /Address2: " + rtxtAddress2.Text)
                    .Append(" /City: " + rtxtCity.Text).Append(" /State: " + rddlState.SelectedItem)
                    .Append(" /Zip:" + rtxtZip.Text)
                    .ToString();

            // setup stuff for both credit and debit payments  -- THIS CAN NOT STAY HARD CODED
            sMerchantReceipt = oSettings.MPReceipt;

            sb.Append(oSettings.MPSettings).Append(" /Amount:" + sTotalAmount)
           .Append(" /Memo:" + txtLoanNumber.Text).Append(sMerchantReceipt)
           .Append(" /Email:" + rtxtEmail.Text).Append(" /CCName:" + rtxtPayerName.Text).Append(" /CCNumber:" + rtxtCCNumber.Text)
           .Append(sCustomerDemoInfo).Append(" " + oSettings.MPAccount);


            results = PostUI.ShowCreditCardForm(sb.ToString());

            //Kill the PostUI object
            PostUI = null;

            if (results.IndexOf("NONE") > 0)   //user cancelled first mile app, leave form state alone
            {
                rtxtPaymentResult.Text = "Cancelled by User.";
                return "";
            }
            if (results.IndexOf("DECLINED") > 0)  //leave form state alone
            {
                rtxtPaymentResult.Text = results;
                return "";
            }
            if (results.IndexOf("SUCCESS") > 0) //post to NLS and close form
            {
                rbtnOpenPayClient.Enabled = false;

                rWizMakeAPayment.BackButton.Enabled = false;
                rWizMakeAPayment.CancelButton.Text = "Close";

                StatusArray = results.Replace("\r\n", ",").Split(',');
                string sRetry = "";
                sPaymentReference = StatusArray[7].ToString();
                sPaymentReference = sPaymentReference.Replace("ORDERID=", "");

                IncludeFees = bool.Parse(oSettings.ChargeFees.ToString());  //for Avid, no fees with transactions
                

                //12-17-2014 This will not work, need to fix it and update the client
                //added while loop to handle retry
                string nlsRetVal = nls.PayByCCDebit(txtLoanNumber.Text, "", sTotalAmount, sTotalFeeAmount, sPaymentReference, this.NLSUserId, IncludeFees.ToString(), results);
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
                    strNLSPostingErrMsg = "An error occured posting a payment to NLS.  Loan Number: " + txtLoanNumber.Text + " Merchant Partners Order Id: " + sPaymentReference + " Payment Amount: " + sTotalAmount + " Collector Number: " + sNLSUserId;
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
                        return "";
                    }

                }
                else
                {
                    if (nlsRetVal == "True")
                    {
                        rtxtPaymentResult.Text = "Payment Posted to Nortridge.";
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
                            rtxtPaymentResult.Text = "Payment Posted to NLS.";
                            this.Close();
                        }
                    }
                }


                rtxtPaymentResult.Text += results;
                //DisableForm();
                //rchtbPayConfirm.Enabled = true;
                //rbtnSaveComment.Enabled = true;

                return "";
            }
            return "";
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
                    rbtnOpenPayClient.Enabled = false;

                }
                else
                {
                    rtxtPaymentResult.Text = "ERROR: ACH Payment Failed with the following error:  " + sPaymentReference[1];
                    rbtnOpenPayClient.Enabled = true;
                }
            }
            else
            {
                return sPaymentReference[0];
            }
            return rtxtPaymentResult.Text;

        }
        private string MakePaymentCheck()
        {
           
            RDSSNLSMPUtilsClasses.cEMail oMail = null;
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile.ToString());

            string MPFirstMileSettings = oSettings.MPSettings;
            string sTotalAmount = "", sRetry="";
            string sTotalFeeAmount = "", sMerchantReceipt = "", sCustomerDemoInfo="";
            string sPaymentPostingEmail = "";
            string strNLSPostingErrMsg = "";
            string sNLSUserId = "";
            string results = "";
            string sPaymentReference = "";
            string nlsRetVal = "";
            bool IncludeFees = false;

            string[] StatusArray;

            RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            ATSSecurePostUILib.ATSSecurePostUI PostUI = null;

            StringBuilder sb = new StringBuilder();
            StringBuilder sb_merchant = new StringBuilder();
            StringBuilder sb_customerdemo = new StringBuilder();

            string LegalEntity = nls.GetLoanGroupByLoan(txtLoanNumber.Text);
            if (ckbxFees.Checked)
            {
                IncludeFees = false;
            }
            else
            {
                IncludeFees = true;
            }

            PostUI = new ATSSecurePostUILib.ATSSecurePostUI(); //Merchant Partners First Mile Assembly

            sTotalAmount = rtxtTotalAmount.Text.Substring(1, rtxtTotalAmount.Text.Length - 1);
            sTotalFeeAmount = rtxtFeeAmt.Text;

            //strip comma from numbers larger than 999.99
            sTotalAmount = Double.Parse(sTotalAmount).ToString();

            sCustomerDemoInfo = sb_customerdemo.Append(" /Address1:" + rtxtAddress1.Text).Append(" /Address2: " + rtxtAddress2.Text)
                    .Append(" /City: " + rtxtCity.Text).Append(" /State: " + rddlState.SelectedItem)
                    .Append(" /Zip:" + rtxtZip.Text)
                    .ToString();

                    // setup stuff for both credit and debit payments  -- THIS CAN NOT STAY HARD CODED
                    sMerchantReceipt = oSettings.MPReceipt;

                    sb.Append(oSettings.MPSettings).Append(" /Amount:" + sTotalAmount)
                   .Append(" /Memo:" + txtLoanNumber.Text).Append(sMerchantReceipt)
                   .Append(" /Email:" + rtxtEmail.Text).Append(" /RoutingNumber:" + rtxtABANumber.Text).Append(" /AccountNumber:" + rtxtBankAccount.Text).Append(" /AccountName:" + rtxtPayerName.Text)
                   .Append(sCustomerDemoInfo).Append(" " + oSettings.MPAccount);


                    results = PostUI.ShowCheckForm(sb.ToString()); //blocks thread and execution stops until First Mile dialog is committed.

                    //Kill the PostUI object
                    PostUI = null;

                    if (results.IndexOf("NONE") > 0)   //user cancelled first mile app, leave form state alone
                    {
                        rtxtPaymentResult.Text = "Cancelled by User.";
                        return "";
                    }
                    if (results.IndexOf("DECLINED") > 0)  //leave form state alone
                    {
                        rtxtPaymentResult.Text = results;
                        return "";
                    }
                    if (results.IndexOf("SUCCESS") > 0) //post to NLS and close form
                    {
                        rbtnOpenPayClient.Enabled = false;

                        rWizMakeAPayment.BackButton.Enabled = false;
                        rWizMakeAPayment.CancelButton.Text = "Close";

                        StatusArray = results.Replace("\r\n", ",").Split(',');

                        sPaymentReference = StatusArray[6].ToString();
                        sPaymentReference = sPaymentReference.Replace("ORDERID=", "");

                        IncludeFees = bool.Parse(oSettings.ChargeFees.ToString());  //for Avid, no fees with transactions

                        while (sRetry == "" )
                        {
                            nlsRetVal = nls.PayByCheck(txtLoanNumber.Text, "", "", sTotalAmount, sTotalFeeAmount, sPaymentReference, sNLSUserId, IncludeFees.ToString(), results);
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
                        if (nlsRetVal.IndexOf("NORTRIDGE ERROR") > 0)
                        {

                            //sPaymentPostingEmail = oSettings.
                            strNLSPostingErrMsg = "An error occured posting a payment to NLS.  Loan Number: " + txtLoanNumber.Text + " Merchant Partners Order Id: " + sPaymentReference + " Payment Amount: " + sTotalAmount + " Collector Number: " + sNLSUserId;
                            oMail = new RDSSNLSMPUtilsClasses.cEMail();

                            //send payment processing the error so it can be manually posted to NLS.  If email will not work, show the message to the collector
                            try
                            {   
                                oMail.EmailCustomerReceipt(sPaymentPostingEmail, oSettings.NLSPaymentPostingErrorEmail, "NORTRIDGE PAYMENT ERROR (ACH)", strNLSPostingErrMsg);
                                rtxtPaymentResult.Text = "An error occured posting a payment to Nortridge.  Email sent to payment processing.";
                            }
                            catch 
                            {
                                rtxtPaymentResult.Text = "Payment not posted to Nortridge.  Email to payment processing failed.  Please notify payment processing with information needed to post payment.";
                                return "";
                            }

                        }
                        else
                        {
                            if (nlsRetVal == "True")
                            {
                                rtxtPaymentResult.Text = "Payment Posted to Nortridge.\r\n";
                                nls.SaveComment(txtLoanNumber.Text, "A Payment Has Been Made:  Amount: " + sTotalAmount + ", OrderId: " + sPaymentReference + "\r\n Results: " + results,sNLSUserId,"<default>");
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
                                    rtxtPaymentResult.Text = "Payment Posted to NLS.\r\n";
                                    this.Close();
                                }
                            }
                        }


                        rtxtPaymentResult.Text += results;


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
                gbxPaymentType.Enabled = true;
                rbtnCrDebit.CheckState = CheckState.Checked;
                rtxtFeeAmt.Text = oSettings.ACHFeeAmount;

                rlblABANumber.Enabled = false;
                rtxtABANumber.Enabled = false;
                rlblBankAccount.Enabled = false;
                rtxtBankAccount.Enabled = false;
                rddlChkSav.Visible = false;
                rdtpPaymentDate.Visible = false;
                rlblChkSav.Visible = false;
                rlblPaymentDate.Visible = false;
                rlblCCNumber.Enabled = true;
                rtxtCCNumber.Enabled = true;


                if (oSettings.ChargeFees.ToString() == "false")  //avidac does not charge convenience fees
                {
                    ckbxFees.Visible = false;
                    rtxtFeeAmt.Visible = false;
                    rtxtFeeAmt.Text = "0.00";
                }

                //Is ACH enabled or disabled?
                bool AchEnabled = oNLS.CheckACHFlag(txtLoanNumber.Text);
                if (AchEnabled == false)
                {
                    rlblAlertMessage.ForeColor = System.Drawing.Color.Red;
                    rlblAlertMessage.Text = "ACH PROCESSING FOR THIS CUSTOMER HAS BEEN DISABLED.  SEE LOAN COMMENTS.";
                    rbtnCrDebit.CheckState = CheckState.Checked;

                    #region Do the credit debit toggle event
                    rlblCCNumber.Enabled = true;
                    rtxtCCNumber.Enabled = true;


                    rlblABANumber.Enabled = false;
                    rlblBankAccount.Enabled = false;
                    rtxtABANumber.Enabled = false;
                    rtxtBankAccount.Enabled = false;


                    rtxtFeeAmt.Text = oSettings.CCDebitFeeAmount.ToString();
                    rtxtTotalAmount.Text = "$" + rtxtFeeAmt.Text;

                    if (this.CurrentState != null)
                        TexasCheck(this.CurrentState);

                    rtxtCCNumber.Focus();
                    #endregion

                }
                rbtnCheck.Enabled = AchEnabled;

                rtxtCCNumber.Focus();

            }
            if (this.rWizMakeAPayment.SelectedPage == this.rWizMakeAPayment.Pages[2]) //Check/CC Form
            {
                

                if (!ValidateCCCheckFields())
                {
                    e.Cancel = true;
                    return;
                }
                
                if (rbtnCheck.CheckState == CheckState.Checked )
                    PAN = rtxtBankAccount.Text;
                else
                    PAN = rtxtCCNumber.Text;

                

                rtxtConfirmMsg.Text = "A payment in the amount of " + rtxtTotalAmount.Text + " is about to be applied to loan number " + txtLoanNumber.Text + 
                    " using Card or Bank Account number " + PAN + ".\r\n";
                rtxtConfirmMsg.Text += "\r\n";
                rtxtConfirmMsg.Text += rtxtCustomerName.Text + "\r\n";
                rtxtConfirmMsg.Text += rtxtAddress1.Text + "\r\n";
                rtxtConfirmMsg.Text += rtxtCity.Text + " " + rddlState.Text + " " + rtxtZip.Text;
                rtxtConfirmMsg.Text += "Click Open Payment Windows to complete this transaction or use the Back button to make changes.";

                rWizMakeAPayment.FinishButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;

                rbtnOpenPayClient.Visibility = Telerik.WinControls.ElementVisibility.Visible;
               

                if (rbtnCheck.CheckState == CheckState.Checked)
                    rbtnOpenPayClient.Text = "Post Payment";

                oSettings = null;
                oNLS = null;
            }
        }
        private void RDSSMakePaymentParent_Load(object sender, EventArgs e)
        {
            RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);
            string CurrentCompany = oSettings.Company;

            System.Drawing.Image imglogo = RDDSMakePayments.Properties.Resources.avid_logo;

            rWizMakeAPayment.NextButton.Enabled = false;
            rlblLogonId.Text = "Welcome back: " + this.LogonId; 
            LoadStatesDDL();
        }
        void TexasCheck(string State)
        {
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);
            //TEXAS CHECK
            if (State.ToUpper() == "TX")
            {
                rtxtFeeAmt.Text = "0";
                rtxtTotalAmount.Text = "0";
                rtxtFeeAmt.Enabled = false;
                ckbxFees.CheckState = CheckState.Checked;
                ckbxFees.Enabled = false;
            }
            else
            {
                if (rbtnCheck.CheckState == CheckState.Checked)
                    rtxtFeeAmt.Text = oSettings.ACHFeeAmount;
                else
                    rtxtFeeAmt.Text = oSettings.CCDebitFeeAmount;

                rtxtFeeAmt.Enabled = true;
                ckbxFees.CheckState = CheckState.Unchecked;
                ckbxFees.Enabled = true;

            }
        }
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

            rtxtTotalAmount.Text = (double.Parse(PaymentAmt) + double.Parse(rtxtFeeAmt.Text)).ToString();
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

            rlblCCNumber.Enabled = true;
            rtxtCCNumber.Enabled = true;
            //rlblCVV.Enabled = true;
            //rtxtCVV.Enabled = true;

            rlblABANumber.Enabled = false;
            rlblBankAccount.Enabled = false;
            rtxtABANumber.Enabled = false;
            rtxtBankAccount.Enabled = false;


            rtxtABANumber.Text = "";
            rtxtBankAccount.Text = "";

            if (oSettings.ChargeFees == "true")
            {
                rtxtFeeAmt.Text = oSettings.CCDebitFeeAmount;
                rtxtTotalAmount.Text = "$" + rtxtFeeAmt.Text;
            }

            if (this.CurrentState != null)
                TexasCheck(this.CurrentState);

            rlblPaymentDate.Visible = false;
            rdtpPaymentDate.Visible = false;
            rddlChkSav.Visible = false;
            rlblChkSav.Visible = false;

            picbxABAVerif.Visible = false;
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

            rlblCCNumber.Enabled = false;
            rtxtCCNumber.Enabled = false;
            //rlblCVV.Enabled = false;
            //rtxtCVV.Enabled = false;

            rlblABANumber.Enabled = true;
            rlblBankAccount.Enabled = true;
            rtxtABANumber.Enabled = true;
            rtxtBankAccount.Enabled = true;


            if (oSettings.ChargeFees == "true")
            {
                rtxtFeeAmt.Text = oSettings.ACHFeeAmount;
                rtxtTotalAmount.Text = "$" + rtxtFeeAmt.Text;
            }

            ckbxFees.Checked = false;

            rtxtCCNumber.Text = "";
            //rtxtCVV.Text = "";


            ckbxFees.TabStop = false;
            rtxtFeeAmt.TabStop = false;

            if (this.CurrentState != null)
                TexasCheck(this.CurrentState);

            rdtpPaymentDate.MinDate = DateTime.Today;
            rdtpPaymentDate.MaxDate = DateTime.Today.AddDays(14);
            rlblPaymentDate.Visible = true;
            rdtpPaymentDate.Visible = true;

            rddlChkSav.Visible = true;
            rlblChkSav.Visible = true;

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


                if (oSettings.ChargeFees == "true")
                {
                    PaymentAmt_Fees = decimal.Parse(rtxtFeeAmt.Text);
                    PaymentAmt = PaymentAmt_Dollars + PaymentAmt_Cents + PaymentAmt_Fees;
                }
                else
                {
                    PaymentAmt = PaymentAmt_Dollars + PaymentAmt_Cents + 0;
                }

                rtxtTotalAmount.Text = PaymentAmt.ToString("C");
                rWizMakeAPayment.NextButton.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                rWizMakeAPayment.NextButton.Enabled = false;
            }
        }

        private void ckbxFees_CheckedChanged(object sender, EventArgs e)
        {
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);

            string strFeeAmount = "";
            if (!ckbxFees.Checked)
            {
                if (rbtnCheck.CheckState == CheckState.Checked)
                {
                    strFeeAmount = oSettings.ACHFeeAmount;
                    rWizMakeAPayment.NextButton.Enabled = false;
                }
                else
                {
                    strFeeAmount = oSettings.CCDebitFeeAmount;
                    rWizMakeAPayment.NextButton.Enabled = false;
                }
            }
            else
            {
                strFeeAmount = "0";
            }
            rtxtFeeAmt.Text = strFeeAmount;
            rWizMakeAPayment.NextButton.Enabled = false;
            oSettings = null;
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
            string ACHType = "NACHA", sRetVal = "" ;

            rbtnOpenPayClient.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            rWizMakeAPayment.BackButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;

            if (rbtnCheck.CheckState == CheckState.Checked)
            {
                if (ACHType == "NACHA")
                {
                    sRetVal = this.MakePaymentACHNacha();
                }
                else
                {
                   sRetVal =  this.MakePaymentCheck();
                }
            }
            
            if (rbtnCrDebit.CheckState == CheckState.Checked)
            {
                   sRetVal= this.MakePaymentCredit();
            }

            //if there is an error then set the enabled and visibility settings to allow clerk to make changes.
            if (sRetVal.IndexOf("ERROR") > -1)
            {
                rbtnOpenPayClient.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                rWizMakeAPayment.BackButton.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                rWizMakeAPayment.CancelButton.Text = "Cancel";
            }
            else
            {
                rWizMakeAPayment.CancelButton.Text = "Finish";
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
            RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            RDSSNLSMPUtilsClasses.cSettings osettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);
            string[] VerifyACH = nls.VerifyACH(osettings.MPAccount.ToString(),float.Parse("0.00"),rtxtABANumber.Text,"123456789",rtxtPayerName.Text,"TEL","1");

            System.Drawing.Image imggreen = RDDSMakePayments.Properties.Resources.blank_green_icon;
            System.Drawing.Image imgyellow = RDDSMakePayments.Properties.Resources.blank_yellow_icon;

            if (VerifyACH[0].ToString() == "VERIFICATION")
            {
                picbxABAVerif.Image = imggreen;
                picbxABAVerif.Visible = true;
            }
            else
            {
                picbxABAVerif.Image = imgyellow;
                picbxABAVerif.Visible = true;
            }
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
       

        
    }
     
}
