using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
                rlblErrorMsg.Text = lgnEx.Message;
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
                }

            }
        }

        private void rfrmLogin_Load(object sender, EventArgs e)
        {
            RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile);
            this.Text   = "RDSS Make Payment: Current Environment is " + oSettings.TestMode.ToUpper();

        }
    }
}
