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

    public partial class rfrmEasyBook : Telerik.WinControls.UI.RadForm
    {
        public rMainWindowFrame ReferredBy { get; set; }

        public rfrmEasyBook()
        {
            InitializeComponent();
        }

        private void rfrmEasyBook_Load(object sender, EventArgs e)
        {
            rtxtName.TextBoxElement.Border.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
        }

        private void rbtnClose_Click(object sender, EventArgs e)
        {
            ReferredBy.rebookloanfrm = null;
            this.Close();
        }

        
    }
}
