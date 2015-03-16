using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.IO;

namespace RDDSMakePayments
{
    public partial class rfrmSettings : Telerik.WinControls.UI.RadForm
    {
        public rfrmSettings()
        {
            InitializeComponent();
        }

        private void rfrmSettings_Load(object sender, EventArgs e)
        {
           
            var fileName = Path.Combine(@"c:\rdss\settings\", "RDSSPaymentConfig.xml");
            DataSet ds = new DataSet();
            System.Xml.XmlDataDocument xdoc = new System.Xml.XmlDataDocument();
            xdoc.Load(fileName);
            System.Xml.XmlReader xreader = System.Xml.XmlReader.Create(new System.IO.StringReader(xdoc.OuterXml));
            xdoc.DataSet.ReadXml(xreader);
            ds = xdoc.DataSet;
            radGridView1.AutoGenerateColumns = true;
            radGridView1.DataSource = ds;

        }
    }
}
