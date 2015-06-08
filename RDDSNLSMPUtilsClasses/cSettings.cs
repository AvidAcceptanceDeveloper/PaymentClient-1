using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RDSSNLSMPUtilsClasses
{
    public class cSettings : XmlDocument
    {
        //XmlDocument xmlConfigDocs = new XmlDocument();
        string sFilePath = "";
        string sEnvironment = "";

        public cSettings(string sPath)
        {
            try
            {
                LoadXmlSettingsDocument(sPath);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                throw ex;
            }
            catch (XmlException xmlexc)
            {
                throw xmlexc;
            }
            
        }
        void LoadXmlSettingsDocument(string sPath)
        {
            try
            {
                this.Load(sPath);
                //is this test of production?
                this.TestMode = this.SelectSingleNode(@"//rdss").Attributes[0].Value;
                if (TestMode == "test")
                {
                    sEnvironment = "test";
                }
                else
                {
                    sEnvironment = "production";
                }

                //get current company
                this.Company = this.SelectSingleNode(@"//company").Attributes["id"].InnerText;
                //general NLS settings
                this.NLSUserId = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/nlssettings").Attributes["user"].InnerText;
                this.NLSPassword = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/nlssettings").Attributes["password"].InnerText; //need to tripledes encrypt
                this.NLSServer = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/nlssettings").Attributes["server"].InnerText;
                this.NLSDatabase = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/nlssettings").Attributes["database"].InnerText;
                this.NLSDomain = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/nlssettings").Attributes["domain"].InnerText;
                this.NLSDatabaseType = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/nlssettings").Attributes["databasetype"].InnerText;
                this.NLSKey = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/nlssettings").Attributes["nlskey"].InnerText;
                this.NLSWebServiceUrl = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/nlssettings").Attributes["webserviceurl"].InnerText;
                //report server
                this.NLSRSDatabase = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/sqlreportserver").Attributes["database"].InnerText;
                this.NLSRSServer = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/sqlreportserver").Attributes["server"].InnerText;
                this.NLSRSUserID = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/sqlreportserver").Attributes["rsuser"].InnerText;
                this.NLSRSPassword = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/sqlreportserver").Attributes["rspassword"].InnerText; //need to tripledes encrypt
                //smtp settings
                this.smtphost = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/emailsettings").Attributes["smtphost"].InnerText;
                this.smtpuserid = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/emailsettings").Attributes["smtpuserid"].InnerText;
                this.smtppassword = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/emailsettings").Attributes["smtppassword"].InnerText;
                this.smtpport = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/generalappsettings/emailsettings").Attributes["smtpport"].InnerText;
                //merchant partner first mile settings
                this.MPAccount = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/merchantpartners[@type='account']").Attributes["value"].InnerText;
                this.MPSettings = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/merchantpartners[@type='application']").Attributes["value"].InnerText;
                //Emails
                this.MPReceipt = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/merchantreceipt").Attributes["value"].InnerText;
                //payment queue settings
                this.PayQuePath = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/paymentqueue").InnerText;
                this.QueErrorPayments = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/paymentqueue").Attributes["queerrpayments"].InnerText;
                this.QueAllPayemnts = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/paymentqueue").Attributes["queallpayments"].InnerText;
                //application stamped comment
                this.PayCommentCategory = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/nlsfields").Attributes["paymentcommentcategory"].InnerText;
                //Fees
                this.ChargeFees = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/fees[@type='general']").Attributes["chargepaymentfees"].InnerText;
                //this.ACHBillingTransaction = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/fees[@type='billcheckfee']").Attributes["transactioncode"].InnerText;
                //this.ACHBillingAmount = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/fees[@type='billcheckfee']").Attributes["transactionfee"].InnerText;
                //this.ACHPaymentTransaction = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/fees[@type='paycheckfee']").Attributes["transactioncode"].InnerText;
                //this.ACHFeeAmount = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/fees[@type='paycheckfee']").Attributes["transactionfee"].InnerText;
                //nls transaction types
                //this.CCDebitBillingTransaction = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/fees[@type='billccdebitfee']").Attributes["transactioncode"].InnerText;
                //this.CCDebitBillingAmount = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/fees[@type='billccdebitfee']").Attributes["transactionfee"].InnerText;
                //this.CCDebitPaymentTransaction = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/fees[@type='payccdebitfee']").Attributes["transactioncode"].InnerText;
                //this.CCDebitFeeAmount = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/fees[@type='payccdebitfee']").Attributes["transactionfee"].InnerText;
                //email addresses
                this.CustomerServiceEmail = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/emailsettings/address[@type='customerservice']").Attributes["address"].InnerText;
                this.CustomerServiceEmail = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/emailsettings/address[@type='nlspostingerrors']").Attributes["address"].InnerText;
                //nls customer udf settings
                this.ACHAllowedField = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/nlsfields").Attributes["achallowedflag"].InnerText;
                //this.ACHAllowedActive = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/nlsfields/achallowedflag").Attributes["active"].InnerText;

                //NLS Payment Methods
                this.ACHPaymentMethod = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/nlspaymentmethods").Attributes["ach"].InnerText;
                this.CreditPaymentMethod = this.SelectSingleNode(@"//rdss/company/appsettings[@environment='" + sEnvironment + "']/generalappsettings/nlspaymentmethods").Attributes["credit"].InnerText;
            }
            catch (XmlException xmlexc)
            {
                throw xmlexc;
            }
            catch (System.IO.FileNotFoundException fnfexc)
            {
                
                throw  fnfexc;
            }
        }
        public string GetMPSettings(string LoanGroup)
        {

            string sEnvironment = "";
            //is this test?
            if (this.TestMode == "test")
            {
                sEnvironment = "test";
            }
            else
            {
                sEnvironment = "production";
            }
            ////appsettings[@environment='test']/merchantpartners[@type="account"][@loangroup="PASP II"]
            this.MPSettings = this.SelectSingleNode(@"//appsettings[@environment='" + sEnvironment + "']/merchantpartners[@type='account'][@loangroup='" + LoanGroup + "']").Attributes["value"].InnerText;
            return this.MPSettings;
        }

        public string GetPropertyValue(string sPropertyName)
        {
            try
            {
                string sRetVal = this.SelectSingleNode("//" + sPropertyName).InnerText;
                if (sRetVal != "")
                {
                    return sRetVal;
                }
                else
                {
                    return "NOTFOUND";
                }
            }
            catch
            {
                return "NOTFOUND_EXC";
            }
        }
        System.Collections.Generic.IDictionary<string, string> ReadAllSettings(XmlDocument xSettingsDoc)
        {
            Dictionary<string, string> oDictionary = new Dictionary<string, string>();

            return oDictionary;
        }

        public string TestMode { get; set; }
        public string Company { get; set; }
        public string NLSUserId { get; set; }
        public string NLSServer { get; set; }
        public string NLSPassword { get; set; }
        public string NLSDatabase { get; set; }
        public string NLSDomain { get; set; }
        public string NLSWebServiceUrl { get; set; }
        public string NLSDatabaseType {get;set;}
        public string NLSKey { get; set; }

        public string NLSRSServer { get; set; }
        public string NLSRSUserID { get; set; }
        public string NLSRSPassword { get; set; }
        public string NLSRSDatabase { get; set; }

        public Dictionary<string, string> mpaccount { get; set; }
        public Dictionary<string, string> feestateexclude { get; set; }
   
        public string PayCommentCategory { get; set; }

        public string smtphost { get; set; }
        public string smtpuserid { get; set; }
        public string smtppassword { get; set; }
        public string smtpport { get; set; }

        public string MPSettings { get; set; }
        public string MPSettingsTransType { get; set; }
        public string MPTransAmount { get; set; }

        public string MPReceipt { get; set; }

        public string PayQuePath { get; set; }
        public string QueErrorPayments { get; set; }
        public string QueAllPayemnts { get; set; }

        //fees
        public string ChargeFees { get; set; }
        public string ACHFeeAmount { get; set; }
        public string CCDebitFeeAmount { get; set; }
        public string ACHBillingTransaction { get; set; }
        public string ACHBillingAmount { get; set; }
        public string ACHPaymentTransaction { get; set; }
        public string CCDebitBillingTransaction { get; set; }
        public string CCDebitBillingAmount { get; set; }
        public string CCDebitPaymentTransaction { get; set; }

        //emails
        public string NLSPaymentPostingErrorEmail { get; set; }
        public string CustomerServiceEmail { get; set; }

        public string ACHAllowedField { get; set; }

        //NLS Payment Methods
        public string ACHPaymentMethod { get; set; }
        public string CreditPaymentMethod { get; set; }

        public string MPAccount { get; set; }
        public string MPMerchKey { get; set; }

        //public string ACHAllowedActive { get; set; }
    }
}
