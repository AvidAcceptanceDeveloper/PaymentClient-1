using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace RDDSMakePayments
{
    public partial class rMainWindowFrame : Telerik.WinControls.UI.RadForm
    {
        public string LogonToken { get; set; }
        public string LogonId { get; set; }
        public bool TakePayments { get; set; }
        public Form SendingForm { get; set; }
        public string NLSUserId { get; set; }

        public rMainWindowFrame()
        {
            InitializeComponent();
        }

        private void rbtnTakePayment_Click(object sender, EventArgs e)
        {
            RDSSMakePaymentParent rmpp = new RDSSMakePaymentParent();
            rmpp.MdiParent = rMainWindowFrame.ActiveForm;
            rmpp.SendingForm = this;

            rmpp.LogonId = this.LogonId;
            rmpp.LogonToken = this.LogonToken;
            rmpp.NLSUserId = this.NLSUserId;

            rmpp.Show();
        }

        private void rMainWindowFrame_Load(object sender, EventArgs e)
        {
            try
            {
                //Get reference to the settings object
                RDSSNLSMPUtilsClasses.cSettings oSettings = new RDSSNLSMPUtilsClasses.cSettings(Properties.Settings.Default.SettingsFile.ToString());

                //Check logon token
                if (this.LogonToken.Length == 0)
                {
                    MessageBox.Show("You must log in first.");
                    this.Dispose();
                }
                if (this.TakePayments == true)
                    rbtnTakePayment.Enabled = true;
                else
                    rbtnTakePayment.Enabled = false;

            }
            catch (System.IO.FileNotFoundException fileexc)
            {
                MessageBox.Show("Configuration File Not Found.");
                this.Dispose();
            }
            catch
            {
                MessageBox.Show("Error occurred loading configuration file.");
                this.Dispose();
            }
        }

     

        private void rbtnCloseApp_Click(object sender, EventArgs e)
        {
            
        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            rfrmSettings settingsfrm = new rfrmSettings();
            settingsfrm.MdiParent = rMainWindowFrame.ActiveForm;
            settingsfrm.Show();
        }

        private void radRibbonBarGroup1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void radButtonElement4_Click(object sender, EventArgs e)
        {
            rfrmLogin.ActiveForm.Dispose();
            SendingForm.Dispose();
        }

       
    }
}
