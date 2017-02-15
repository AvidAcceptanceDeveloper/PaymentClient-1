using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RDDSMakePayments
{
    public partial class rfrmLogin : Telerik.WinControls.UI.ShapedForm
    {
        public rfrmLogin()
        {
            InitializeComponent();
        }

        private void rbtnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void rbtnLogin_Click(object sender, EventArgs e)
        {
            string sToken = "";
            RDSSNLSMPUtilsClasses.cNortridgeWapper nls = new RDSSNLSMPUtilsClasses.cNortridgeWapper();
            
            try
            {
                sToken = nls.LoginContact(rtxtUserId.Text, rtxtPassword.Text);
            }
            catch (Exception lgnEx)
            {
                rlblErrorMsg.Text = "Error obtaining log-in token from NLS. ERROR: " + lgnEx.Message;

                rbtnCancel.Enabled = true;
                rbtnLogin.Enabled = true;

                return;
            }
            if (sToken.Length > 0)
            {
                if (sToken.IndexOf("ERRORCODE")==-1)
                {
                    //Figure out which applications to enable
                    string ContactNumber = nls.GetContactNumberByWebCredentials(rtxtUserId.Text);
                    bool TakePayments = nls.CanTakePayments(sToken, ContactNumber);
                    string NLSUserId = nls.GetNLSUserId(sToken, ContactNumber);
                    rMainWindowFrame rmainframe = new rMainWindowFrame();
                    rmainframe.LogonToken = sToken;
                    rmainframe.TakePayments = TakePayments;
                    rmainframe.LogonId = rtxtUserId.Text;

                    rmainframe.SendingForm = this;
                    rmainframe.NLSUserId = NLSUserId; ;
                    rmainframe.Show();

                    this.Hide();
                }
                else
                {
                    rlblErrorMsg.Text = "Error logging into system.\r\nCheck your user id and password and try again.";
                    rbtnCancel.Enabled = true;
                    rbtnLogin.Enabled = true;
                }

            }
        }

        private void rfrmLogin_Load(object sender, EventArgs e)
        {
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);
            this.Text   = "RDSS Make Payment: Current Environment is " + oSettings.TestMode.ToUpper();

        }
        private void rtxtPassword_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            
                if (e.KeyChar == (char)Keys.Return)

                {
                    rbtnLogin.Enabled = false;
                    rbtnCancel.Enabled = false;
                    rbtnLogin_Click(sender, e);
                }
           
        }
        
        private void DisplayLoginMessage()
        {
            rlblErrorMsg.Text = "Logging In, Please Wait.";
        }
        private void rtxtUserId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
