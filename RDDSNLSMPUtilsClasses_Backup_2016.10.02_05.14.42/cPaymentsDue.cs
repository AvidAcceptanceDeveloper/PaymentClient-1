using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RDSSNLSMPUtilsClasses
{
    public class cPaymentsDue
    {
        public string AcctRefNo { get; set; }
        public string DateDue { get; set; }
        public string PaymentNumber { get; set; }
        public string PaymentDescription { get; set; }
        public string PaymentPaid { get; set; }
        public string PaymentRemaining { get; set; }

        public void Load(XmlNode xNode)
        {
            if (xNode.HasChildNodes)
            {
                this.AcctRefNo = xNode.SelectSingleNode("//acctrefno").InnerText;
                this.DateDue = xNode.SelectSingleNode("//date_due").InnerText;
                this.PaymentNumber = xNode.SelectSingleNode("//payment_number").InnerText;
                this.PaymentDescription = xNode.SelectSingleNode("//payment_description").InnerText;
                this.PaymentPaid = xNode.SelectSingleNode("//payment_paid").InnerText;
                this.PaymentRemaining = xNode.SelectSingleNode("//payment_remaining").InnerText;
            }
        }
    }

}

