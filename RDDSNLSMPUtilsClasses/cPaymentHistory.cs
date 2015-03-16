using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace RDSSNLSMPUtilsClasses
{
    public class cPaymentHistory
    {
        public string AcctRefNo { get; set; }
        public string DateDue { get; set; }
        public string DatePaid { get; set; }
        public string PaymentNumber { get; set; }
        public string PaymentAmount { get; set; }
        public string PaymentDescription { get; set; }
        public string TransactionCode { get; set; }
        public string PaymentMethodCode { get; set; }
        public string PaymentMethodReference { get; set; }
        public string ACHTraceNumber { get; set; }
        public string NSFFlag { get; set; }
        public string NSFDate { get; set; }
        public string UserDef01 { get; set; }
        public string UserDef02 { get; set; }
        public string UserDef03 { get; set; }
        public string UserDef04 { get; set; }
        public string UserDef05 { get; set; }

        public void Load(XmlNode xnode)
        {
            if (xnode.HasChildNodes)
            {

                this.AcctRefNo = xnode.SelectSingleNode("//acctrefno").InnerText;
                this.DateDue = xnode.SelectSingleNode("//date_due").InnerText;
                this.DatePaid = xnode.SelectSingleNode("//date_paid").InnerText;
                this.PaymentNumber = xnode.SelectSingleNode("//payment_number").InnerText;
                this.PaymentAmount = xnode.SelectSingleNode("//payment_amount").InnerText;
                this.PaymentDescription = xnode.SelectSingleNode("//payment_description").InnerText;
                this.TransactionCode = xnode.SelectSingleNode("//transaction_code").InnerText;
                this.PaymentMethodCode = xnode.SelectSingleNode("//payment_method_code").InnerText;
                this.PaymentMethodReference = xnode.SelectSingleNode("//payment_method_reference").InnerText;
                this.ACHTraceNumber = xnode.SelectSingleNode("//ach_trace_number").InnerText;
                this.NSFFlag = xnode.SelectSingleNode("//nsf_flag").InnerText;
                this.NSFDate = xnode.SelectSingleNode("//nsf_date").InnerText;
                this.UserDef01 = xnode.SelectSingleNode("//userdef01").InnerText;
                this.UserDef02 = xnode.SelectSingleNode("//userdef02").InnerText;
                this.UserDef03 = xnode.SelectSingleNode("//userdef03").InnerText;
                this.UserDef04 = xnode.SelectSingleNode("//userdef04").InnerText;
                this.UserDef05 = xnode.SelectSingleNode("//userdef05").InnerText;
            }

        }
        public cPaymentHistory[] ListPaymentHistory(XmlDocument xdoc_PaymentHistory)
        {
            XmlNodeList xnodelist = xdoc_PaymentHistory.SelectNodes("\\NLSWEB");
            int iNodeCount = xnodelist.Count;
            int icntr = 0;
            cPaymentHistory[] cpayhistory = new cPaymentHistory[iNodeCount];
            foreach (XmlNode xnode in xnodelist)
            {
                this.Load(xnode);
                cpayhistory[icntr].Load(xnode);
                icntr++;
            }
            return cpayhistory;
        }

    }
}
