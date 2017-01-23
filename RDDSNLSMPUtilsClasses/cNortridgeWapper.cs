using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nlsws = RDSSNLSMPUtilsClasses.com.cyberridge.ws;
using mp_ws = RDSSNLSMPUtilsClasses.com.merchantpartners;
using System.Xml;
using System.Data;
using System.Data.OleDb;

namespace RDSSNLSMPUtilsClasses
{
    public class cNortridgeWapper
    {
        

        string _nlsuser, _nlspassword, _nlsdomain;
        string _nlsdatabasename, _nlsservername, _nlsdatabasetype, _nlskey;
        RDSSNLSMPUtilsClasses.cSettings oSettings = new cSettings(Properties.Settings.Default.SettingsFile.ToString());

        

        public string GetLoanGroupByLoan(string LoanNumber)
        {
            try
            {
                RDSSNLSMPUtilsClasses.cData oData = new RDSSNLSMPUtilsClasses.cData();
                oData.AddToParameterCollection(LoanNumber, "WebCredentials");
                string sql = "select b.userdef18 from cif_web a inner join cif_detail b on a.cifno = b.cifno where a.user_name = ?";
                DataSet ds = oData.GetRowsByParameterList(sql, oData.ParamCollection);
                oData.ParamCollection.Clear();
                return ds.Tables[0].Rows[0][0].ToString();

            }
            catch
            {
                return "";
            }
        }

        string GetPropertyValue(string PropertyName)
        {
            return Properties.Settings.Default[PropertyName].ToString();
            // return System.Configuration.ConfigurationManager.AppSettings[PropertyName];

        }
        public string GetContactNumberByWebCredentials(string strUser)
        {
            try
            {
                RDSSNLSMPUtilsClasses.cData oData = new RDSSNLSMPUtilsClasses.cData();
                //oData.AddToParameterCollection(strUser, "user_name");
                string sql = "select b.cifnumber from cif_web a inner join cif b on a.cifno = b.cifno ";
                string strSQLWhere = " a.user_name = '" + strUser + "'";
                DataSet ds = oData.GetRowsByWhereClause(sql, strSQLWhere);

                return ds.Tables[0].Rows[0][0].ToString();

            }
            catch
            {
                return "";
            }
        }
        public string LoginContact(string UserId, string Password)
        {
            nlsws.Service oNLS = GetNLSObject();
            try
            {
                string sToken = oNLS.NLSAuthenticate(this.NLSDataBaseType, this.NLSServerName, this.NLSDatabaseName, UserId, Password, "");

                if (sToken.Length > 0)
                {
                    return sToken;
                }
                else
                {
                    return "";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool CanTakePayments(string sToken, string sContactNumber)
        {
            nlsws.Service oNLS = GetNLSObject();
            string CifInfo = oNLS.NLSGetContact(sToken, sContactNumber);
            System.Xml.XmlDocument oXMLDoc = new System.Xml.XmlDocument();
            oXMLDoc.LoadXml(CifInfo);
            string CifNo = oXMLDoc.SelectSingleNode("//cifno").InnerText;
            string ContactUDF = oNLS.NLSContactUDF(sToken, sContactNumber, "CIF_DETAIL_UDF1");
            oXMLDoc.LoadXml(ContactUDF);
            if (oXMLDoc.SelectSingleNode("//CIF_DETAIL_UDF1").InnerText=="1")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public string GetNLSUserId(string sToken, string sContactNumber)
        {
            nlsws.Service oNLS = GetNLSObject();
            string CifInfo = oNLS.NLSGetContact(sToken, sContactNumber);
            System.Xml.XmlDocument oXMLDoc = new System.Xml.XmlDocument();
            oXMLDoc.LoadXml(CifInfo);
            string CifNo = oXMLDoc.SelectSingleNode("//cifno").InnerText;
            string ContactUDF = oNLS.NLSContactUDF(sToken, sContactNumber, "CIF_DETAIL_UDF18");
            oXMLDoc.LoadXml(ContactUDF);
            return oXMLDoc.SelectSingleNode("//CIF_DETAIL_UDF18").InnerText;
            

        }
        //public Dictionary<string, string> GetMerchantPartnerSettings(string LegalEntity)
        //{
        //    XmlNodeList xnodelist = null;

        //    if (LegalEntity.Length == 0)
        //    {
        //        throw new Exception("LegalEntity has not been setup on Loan Detail 2.UDF47.(Loan Group)");

        //    }
        //    cSettings oSettings = new cSettings(Properties.Settings.Default.SettingsFile);
        //    Dictionary<string, string> dictSettings = new Dictionary<string, string>();
        //    string sTestFlag = oSettings.SelectSingleNode(@"//RDSS[@mode='test']").ToString();
        //    var fileName = "";

        //    if (sTestFlag == "test")
        //    {
        //        xnodelist = oSettings.SelectNodes("//appsettings[@environment = 'test']");
        //    }
        //    else
        //    {
        //        xnodelist = oSettings.SelectNodes("//appsettings[@environment = 'production']");
        //    }

        //    foreach (XmlNode e in xnodelist)
        //    {
        //       XmlNode xnode = e.SelectSingleNode("//merchantpartners[@type='account'][@loangroup='" + LegalEntity + "']");
        //    dictSettings.Add("ACCTID",xnode.Attributes.GetNamedItem("ATSID" ).ToString());
        //    dictSettings.Add("SUBACCTID", xnode.Attributes.GetNamedItem("ATSSubID").ToString());
        //    dictSettings.Add("MERCHANTPIN", xnode.Attributes.GetNamedItem("merchantpin").ToString());
        //    }
            
        //    oSettings = null;
        //    return dictSettings;
        //}
        //public Dictionary<string, string> GetFlashMessages()
        //{
        //    Dictionary<string, string> dictMessages = new Dictionary<string, string>();

        //    // this can not stay hard coded
        //    string sTestFlag = Properties.Settings.Default.TestFlag.ToString().ToUpper();
        //    var fileName = "";

        //    if (sTestFlag == "TRUE")
        //    {
        //        fileName = string.Format(Properties.Settings.Default.TestMPSettings.ToString());
        //    }
        //    else
        //    {
        //        fileName = string.Format(Properties.Settings.Default.MerchantPartnersSettings.ToString());
        //    }
        //    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //    var connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=False;", fileName);
        //    var cmd = new OleDbCommand();
        //    var conn = new OleDbConnection(connectionString);
        //    conn.Open();

        //    var adapter = new OleDbDataAdapter("SELECT * FROM [FlashMessages]", conn);
        //    var ds = new DataSet();

        //    adapter.Fill(ds, "FlashMessages");

        //    System.Data.DataTable data = ds.Tables["FlashMessages"];

        //    dictMessages.Add("FlashMessages", data.Rows[0]["FlashMessages"].ToString());


        //    return dictMessages;
        //}

        public string SaveComment(string strLoanNumber, string strComment, string strUserLoggedIn, string CommentCategory)
        {
            nlsws.Service ws = GetNLSObject();
            StringBuilder sb = new StringBuilder();
            string strErrorMessage = "";
            sb.Clear();
        
            strLoanNumber = strLoanNumber.Replace("\r\n", string.Empty);

            sb.Append("<NLS CommitBlock='0'><LOAN LoanNumber='" + strLoanNumber + "' UpdateFlag='1'>")
                .Append("<LOANCOMMENTS Date = '" + DateTime.Now + "' Comment='" + strComment + "' ")
                .Append("CommentDescription = 'Payment Comment' Category ='" + CommentCategory + "' CreatedBy='0' /></LOAN></NLS>");
            try
            {
                ws.ImportXML(this.NLSServerName, this.NLSDatabaseName, sb.ToString(), out strErrorMessage);
                return strErrorMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        
       
        public string PayByCheck(string sLoanNumber, string abanumber, string checkingaccountnumber,
             string paymentamount, string achfeeamt, string paymentreference, string effectivedate, string transuserid, string IncludeFees, string MPResults)
        {
            XmlDocument xdoc = new XmlDocument();
            //Parse order id from payment reference string

            //paymentreference = paymentreference.Substring(8, 19 - 10);

            nlsws.Service oNLS = GetNLSObject();
            string sImportString = "", ErrMsg = "";
            string sToken = oNLS.NLSGlobalAuthentication(this.NLSDataBaseType, this.NLSServerName, this.NLSDatabaseName);
            bool payretval = false;

            sLoanNumber = sLoanNumber.Replace("\r\n", string.Empty);

            StringBuilder sb = new StringBuilder();

            sb.Append("<NLS CommitBlock='1' EnforceTagExistence='0'>");
            sb.Append("<TRANSACTIONS GLDate='" + effectivedate + "'>");
            sb.Append("<PAYMENT LoanNumber='" + sLoanNumber + "' EffectiveDate='" + effectivedate + "' PaymentMethod='" + oSettings.ACHPaymentMethod.ToString() + "' PaymentMethodReference='PAYMENT APP ACH' Amount='" + paymentamount + "' UserDefined2='" + paymentreference + "' UserDefined1='" + transuserid + "' />");
            sb.Append("</TRANSACTIONS></NLS>");

            sImportString = sb.ToString();
            if (oSettings.QueAllPayemnts == "true")
            {
                return sb.ToString();
            }
            else
            {
                try
                {
                    if (oNLS.ImportXMLTest(this.NLSServerName, this.NLSDatabaseName, sImportString, out  ErrMsg))
                    {
                        payretval = oNLS.ImportXML(this.NLSServerName, this.NLSDatabaseName, sImportString, out  ErrMsg);
                        if (payretval)
                        {
                            this.SaveComment(sLoanNumber, "A Payment has been made. Method = ACH Amount=" + paymentamount, NLSUser, oSettings.PayCommentCategory);
                        }
                        return payretval.ToString();
                    }
                    else
                    {

                        //// if QueErrPayments is true the put the errant payent into a message que for later retries
                        //// else send an email to payment processing for manual submital
                        //if (Properties.Settings.Default.QueErrPayments == true)
                        //    return sb.ToString();
                        //else
                        return payretval.ToString() + "NORTRIDGE ERROR: " + ErrMsg;
                    }
                }
                catch (Exception NLSException)
                {
                    return NLSException.Message;
                }
            }

        }

        //Init a Random Class
        static Random randnbr = new Random();

        public string PayByACHNacha(string sLoanNumber,string sCompanyId, string abanumber, string checkingaccountnumber,
             string paymentamount, string achfeeamt, string paymentreference, string transuserid, string IncludeFees, string MPResults,DateTime PayDate, string sCustomerEmail)
        {
            
            XmlDocument xdoc = new XmlDocument();
            //Parse order id from payment reference string

            //paymentreference = paymentreference.Substring(8, 19 - 10);
            
            nlsws.Service oNLS = GetNLSObject();
            string sImportString = "", ErrMsg = "";
            string sToken = oNLS.NLSGlobalAuthentication(this.NLSDataBaseType, this.NLSServerName, this.NLSDatabaseName);
            cEMail oEmail = new cEMail();
            bool payretval = false;
            int randint = randnbr.Next();

            string sReference = "" + randint + sLoanNumber;

            sLoanNumber = sLoanNumber.Replace("\r\n", string.Empty);

            StringBuilder sb = new StringBuilder();

            sb.Append("<NLS CommitBlock='1' EnforceTagExistence='0'>");
            sb.Append("<LOAN UpdateFlag='1' LoanNumber='" + sLoanNumber + "'>");
            sb.Append("<ACH ACHCompanyID ='" + sCompanyId + "' Status='0' ABANumber ='" + abanumber + 
                "' AccountNumber='" + checkingaccountnumber + "' Amount='" + double.Parse(paymentamount) +
                "' MaximumAmountOfDraws='1' BillingType='1' BillingStartDate='" + PayDate.ToShortDateString() + "' Description='" + sReference + "' OptionFlags='2' />");
            sb.Append("</LOAN></NLS>");

            sImportString = sb.ToString();
            if (oSettings.QueAllPayemnts == "true")
            {
                return sb.ToString();
            }
            else
            {
                try
                {
                    if (oNLS.ImportXMLTest(this.NLSServerName, this.NLSDatabaseName, sImportString, out  ErrMsg))
                    {
                        payretval = oNLS.ImportXML(this.NLSServerName, this.NLSDatabaseName, sImportString, out  ErrMsg);
                        if (payretval)
                        {
                             
                            this.SaveComment(sLoanNumber, "A OneTime ACH NACHA Schedule has been created. Billing Start Date is: " + PayDate.ToShortDateString() + " Method = NACHA Amount=" + paymentamount, NLSUser, oSettings.PayCommentCategory);

                            try
                            {
                                string sSubject = "Payment Confirmation";

                                oEmail.EmailCustomerReceipt(sCustomerEmail, "customerservice@avidac.com", "Payment", "An ACH has been posted to your account in the amount of : $" + paymentamount +  " Reference: " + sReference);
                            }
                            catch(Exception ex)
                            {
                                sReference += "\r\nEmail was not sent due to an exception.\r\n Exception: "+ex.InnerException.Message;
                            }
                        }
                        return payretval.ToString() + "|" + sReference;
                    }
                    else
                    {

                        //// if QueErrPayments is true the put the errant payent into a message que for later retries
                        //// else send an email to payment processing for manual submital
                        //if (Properties.Settings.Default.QueErrPayments == true)
                        //    return sb.ToString();
                        //else
                        return payretval.ToString() + "|" + "NORTRIDGE ERROR: " + ErrMsg;
                    }
                }
                catch (Exception NLSException)
                {
                    return NLSException.Message;
                }
            }

        }

        public string PayByCCDebit(string sLoanNumber, string nameoncard,
             string paymentamount, string crdebitfeeamt, string paymentreference, string effectivedate, string transuserid, string IncludeFees, string Results)
        {

            XmlDocument xdoc = new XmlDocument();

            nlsws.Service oNLS = GetNLSObject();
            string sImportString, ErrMsg;
            string sToken = oNLS.NLSGlobalAuthentication(this.NLSDataBaseType, this.NLSServerName, this.NLSDatabaseName);
            bool payretval = false;

            StringBuilder sb = new StringBuilder();

            //Parse order id from payment reference string
            //paymentreference = paymentreference.Substring(8, 19 - 10);

            sLoanNumber.Replace("\r\n", string.Empty);

            

            sb.Append("<NLS CommitBlock='1' EnforceTagExistence='0'>");
            sb.Append("<TRANSACTIONS GLDate='" + effectivedate + "'>");
            sb.Append("<PAYMENT LoanNumber='" + sLoanNumber + "' EffectiveDate='" + effectivedate + "' PaymentMethod='" + oSettings.CreditPaymentMethod.ToString() + "' PaymentMethodReference='PAYMENT APP CREDIT/DEBIT' Amount='" + paymentamount + "' UserDefined2='" + paymentreference + "' UserDefined1='" + transuserid + "' />");
            sb.Append("</TRANSACTIONS></NLS>");

            sImportString = sb.ToString();

            if (oNLS.ImportXMLTest(this.NLSServerName, this.NLSDatabaseName, sImportString, out  ErrMsg))
            {
                payretval = oNLS.ImportXML(this.NLSServerName, this.NLSDatabaseName, sImportString, out  ErrMsg);
                if (payretval){
                    this.SaveComment(sLoanNumber, "A Payment has been made. Method = CC/DEBIT Amount=" + paymentamount, transuserid, oSettings.PayCommentCategory);
                }
                return payretval.ToString();
            }
            else
            {

                // if QueErrPayments is true the put the errant payent into a message que for later retries
                // else send an email to payment processing for manual submital
                if (oSettings.QueErrorPayments == "true")
                    return sb.ToString();
                else
                    return payretval.ToString() + "NORTRIDGE ERROR: " + ErrMsg;
            }


        }
       
        public cCustomerInfo GetCustomerInformation(string sContactNumber)
        {
            XmlDocument xdoc = new XmlDocument();
            nlsws.Service oNLS = GetNLSObject();
            cCustomerInfo ci = new cCustomerInfo();

            string sToken = oNLS.NLSGlobalAuthentication(this.NLSDataBaseType, this.NLSServerName, this.NLSDatabaseName);
            xdoc.LoadXml(oNLS.NLSGetContact(sToken, sContactNumber));
            ci.Load(xdoc);

            return ci;
        }
        public bool CheckACHFlag(string sLoanNumber)
        {
            return true;
            //cLoanInfo ci = GetLoanDetailInformation(sLoanNumber);
            //if (ci.LoanDetail2UDF.udf33 == "NO")
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}

        }
        public string GetCustomerNumberByLoan(string sLoanNumber)
        {
            try
            {
                RDSSNLSMPUtilsClasses.cData oData = new RDSSNLSMPUtilsClasses.cData();
                oData.AddToParameterCollection(sLoanNumber, "Loan_Number");
                string sql = "select cifnumber from cif inner join loanacct a on cif.cifno = a.cifno where a.loan_number = ?";
                DataSet ds = oData.GetRowsByParameterList(sql, oData.ParamCollection);
                oData.ParamCollection.Clear();
                return ds.Tables[0].Rows[0][0].ToString();

            }
            catch
            {
                return "";
            }

        }
        public string GetCustomerAutoPayFlag(string sLoanNumber)
        {
            try
            {
                cData oData = new cData();
                oData.AddToParameterCollection(sLoanNumber, "Loan_Number_APFlag");
                string sql = "select userdef50 from loanacct_detail a inner join loanacct b on a.acctrefno = b.acctrefno where b.loan_number = ?";
                DataSet ds = oData.GetRowsByParameterList(sql, oData.ParamCollection);
                oData.ParamCollection.Clear();
                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                return "";
            }

        }
        public int GetNLSLoanGroupNo(string sLoanNumber)
        {

            return 0;
        }
        public cLoanInfo GetLoanDetailInformation(string sLoanNumber)
        {
            XmlDocument xdoc = new XmlDocument();
            nlsws.Service oNLS = GetNLSObject();
            cLoanInfo li = new cLoanInfo();

            sLoanNumber = sLoanNumber.Replace("\r\n", string.Empty);


            string sToken = oNLS.NLSGlobalAuthentication(this.NLSDataBaseType, this.NLSServerName, this.NLSDatabaseName);
            xdoc.LoadXml(oNLS.NLSGetLoanDetail(sToken, sLoanNumber));

            string sUDF1 = oNLS.NLSLoanDetailUDF(sToken, sLoanNumber, "LOAN_DETAIL1_UDF1,LOAN_DETAIL1_UDF2,LOAN_DETAIL1_UDF3,LOAN_DETAIL1_UDF4,LOAN_DETAIL1_UDF5,LOAN_DETAIL1_UDF6,LOAN_DETAIL1_UDF7,LOAN_DETAIL1_UDF8,LOAN_DETAIL1_UDF9,LOAN_DETAIL1_UDF10," +
                                                                      "LOAN_DETAIL1_UDF11,LOAN_DETAIL1_UDF12,LOAN_DETAIL1_UDF13,LOAN_DETAIL1_UDF14,LOAN_DETAIL1_UDF15,LOAN_DETAIL1_UDF16,LOAN_DETAIL1_UDF17,LOAN_DETAIL1_UDF18,LOAN_DETAIL1_UDF19,LOAN_DETAIL1_UDF20," +
                                                                      "LOAN_DETAIL1_UDF21,LOAN_DETAIL1_UDF22,LOAN_DETAIL1_UDF23,LOAN_DETAIL1_UDF24,LOAN_DETAIL1_UDF25,LOAN_DETAIL1_UDF26,LOAN_DETAIL1_UDF27,LOAN_DETAIL1_UDF28,LOAN_DETAIL1_UDF29,LOAN_DETAIL1_UDF30," +
                                                                      "LOAN_DETAIL1_UDF31,LOAN_DETAIL1_UDF32,LOAN_DETAIL1_UDF33,LOAN_DETAIL1_UDF34,LOAN_DETAIL1_UDF35,LOAN_DETAIL1_UDF36,LOAN_DETAIL1_UDF37,LOAN_DETAIL1_UDF38,LOAN_DETAIL1_UDF39,LOAN_DETAIL1_UDF40," +
                                                                      "LOAN_DETAIL1_UDF41,LOAN_DETAIL1_UDF42,LOAN_DETAIL1_UDF43,LOAN_DETAIL1_UDF44,LOAN_DETAIL1_UDF45,LOAN_DETAIL1_UDF46,LOAN_DETAIL1_UDF47,LOAN_DETAIL1_UDF48,LOAN_DETAIL1_UDF49,LOAN_DETAIL1_UDF50");


            string sUDF2 = oNLS.NLSLoanDetailUDF(sToken, sLoanNumber, "LOAN_DETAIL2_UDF1,LOAN_DETAIL2_UDF2,LOAN_DETAIL2_UDF3,LOAN_DETAIL2_UDF4,LOAN_DETAIL2_UDF5,LOAN_DETAIL2_UDF6,LOAN_DETAIL2_UDF7,LOAN_DETAIL2_UDF8,LOAN_DETAIL2_UDF9,LOAN_DETAIL2_UDF10," +
                                                                      "LOAN_DETAIL2_UDF11,LOAN_DETAIL2_UDF12,LOAN_DETAIL2_UDF13,LOAN_DETAIL2_UDF14,LOAN_DETAIL2_UDF15,LOAN_DETAIL2_UDF16,LOAN_DETAIL2_UDF17,LOAN_DETAIL2_UDF18,LOAN_DETAIL2_UDF19,LOAN_DETAIL2_UDF20," +
                                                                      "LOAN_DETAIL2_UDF21,LOAN_DETAIL2_UDF22,LOAN_DETAIL2_UDF23,LOAN_DETAIL2_UDF24,LOAN_DETAIL2_UDF25,LOAN_DETAIL2_UDF26,LOAN_DETAIL2_UDF27,LOAN_DETAIL2_UDF28,LOAN_DETAIL2_UDF29,LOAN_DETAIL2_UDF30," +
                                                                      "LOAN_DETAIL2_UDF31,LOAN_DETAIL2_UDF32,LOAN_DETAIL2_UDF33,LOAN_DETAIL2_UDF34,LOAN_DETAIL2_UDF35,LOAN_DETAIL2_UDF36,LOAN_DETAIL2_UDF37,LOAN_DETAIL2_UDF38,LOAN_DETAIL2_UDF39,LOAN_DETAIL2_UDF40," +
                                                                      "LOAN_DETAIL2_UDF41,LOAN_DETAIL2_UDF42,LOAN_DETAIL2_UDF43,LOAN_DETAIL2_UDF44,LOAN_DETAIL2_UDF45,LOAN_DETAIL2_UDF46,LOAN_DETAIL2_UDF47,LOAN_DETAIL2_UDF48,LOAN_DETAIL2_UDF49,LOAN_DETAIL2_UDF50");
            if (xdoc.InnerText == "")
            {
                return null;
            }
            else
            {
                li.Load(xdoc);

                //Composite objects to support LoadDetail1_UDF and LoanDetail2_UDF
                LoanDetail1UDF UDF1 = new LoanDetail1UDF(sUDF1);
                LoanDetail2UDF UDF2 = new LoanDetail2UDF(sUDF2);

                li.LoanDetail1UDF = UDF1;
                li.LoanDetail2UDF = UDF2;
                //////////////////////////////////////////////////////////////////

                return li;
            }


        }


        public string GetLoanList(string sContactNumber)
        {
            XmlDocument xdoc = new XmlDocument();
            nlsws.Service oNLS = GetNLSObject();

            string sToken = oNLS.NLSGlobalAuthentication(this.NLSDataBaseType, this.NLSServerName, this.NLSDatabaseName);
            xdoc.LoadXml(oNLS.NLSGetLoanList(sToken, sContactNumber, false));
            return xdoc.SelectSingleNode("//LoanNumber").InnerText;
        }

        public List<cPaymentHistory> GetPaymentHistory(string sLoanNumber)
        {
            XmlDocument xdoc = new XmlDocument();
            nlsws.Service oNLS = GetNLSObject();
            List<cPaymentHistory> phlist = new List<cPaymentHistory>();
            cPaymentHistory ph = new cPaymentHistory();
            DateTime dtStart = DateTime.Today, dtEnd = DateTime.Today;
            string sStartDt, sEndDt;

            sStartDt = dtStart.AddYears(-1).ToShortDateString();
            sEndDt = dtEnd.ToShortDateString();

            sLoanNumber = sLoanNumber.Replace("\r\n", string.Empty);

            string sToken = oNLS.NLSGlobalAuthentication(this.NLSDataBaseType, this.NLSServerName, this.NLSDatabaseName);
            xdoc.LoadXml(oNLS.NLSLoanPaymentHistory(sToken, sLoanNumber, sStartDt, sEndDt));
            foreach (XmlNode xnode in xdoc.SelectNodes("//NLSWEB"))
            {
                ph.Load(xnode);
                phlist.Add(ph);
            }
            return phlist;

        }
        public List<cPaymentsDue> GetPaymentsDue(string sLoanNumber)
        {
            XmlDocument xdoc = new XmlDocument();
            nlsws.Service oNLS = GetNLSObject();
            List<cPaymentsDue> phlist = new List<cPaymentsDue>();
            cPaymentsDue ph = new cPaymentsDue();
            DateTime dtStart = DateTime.Today, dtEnd = DateTime.Today;
            string sStartDt, sEndDt;

            sStartDt = dtStart.AddYears(-1).ToShortDateString();
            sEndDt = dtEnd.ToShortDateString();

            sLoanNumber = sLoanNumber.Replace("\r\n", string.Empty);

            string sToken = oNLS.NLSGlobalAuthentication(this.NLSDataBaseType, this.NLSServerName, this.NLSDatabaseName);
            xdoc.LoadXml(oNLS.NLSLoanPaymentsDue(sToken, sLoanNumber, "", ""));
            if (xdoc.InnerXml.ToString() == "<NLSWEB></NLSWEB>")
            {
                return null;
            }
            foreach (XmlNode xnode in xdoc.SelectNodes("//NLSWEB"))
            {
                ph.Load(xnode);
                phlist.Add(ph);
            }
            return phlist;

        }
        public cNortridgeWapper()
        {
            cSettings oSettings = new cSettings(Properties.Settings.Default.SettingsFile);

            this.NLSServerName = oSettings.NLSServer;
            this.NLSDatabaseName = oSettings.NLSDatabase;
            this.NLSDomain = oSettings.NLSDomain;
            this.NLSUser = oSettings.NLSUserId;
            this.NLSDataBaseType = oSettings.NLSDatabaseType;
            this.NLSPassword = oSettings.NLSPassword;
            this.NLSKey = oSettings.NLSKey;
            this.NLSWebServiceUrl = oSettings.NLSWebServiceUrl;

        }

        private nlsws.Service GetNLSObject()
        {
            nlsws.Service nlsservice = new nlsws.Service();
            nlsservice.Credentials = new System.Net.NetworkCredential(this.NLSUser, this.NLSPassword, this.NLSDomain);

            return nlsservice;

        }
        private void GetNLSConfiguration()
        {
            this.NLSUser = "";
            this.NLSPassword = "";
            this.NLSDatabaseName = "";
            this.NLSDomain = "";
            this.NLSServerName = "";
            this.NLSKey = "";
            this.NLSWebServiceUrl = "";

        }
        private string NLSUser
        {
            get { return _nlsuser; }
            set { _nlsuser = value; }
        }
        private string NLSPassword
        {
            get { return _nlspassword; }
            set { _nlspassword = value; }
        }
        private string NLSDomain
        {
            get { return _nlsdomain; }
            set { _nlsdomain = value; }
        }
        private string NLSDatabaseName
        {
            get { return _nlsdatabasename; }
            set { _nlsdatabasename = value; }
        }
        private string NLSServerName
        {
            get { return _nlsservername; }
            set { _nlsservername = value; }
        }
        private string NLSDataBaseType
        {
            get { return _nlsdatabasetype; }
            set { _nlsdatabasetype = value; }
        }
        private string NLSKey
        {
            get { return _nlskey; }
            set { _nlskey = value; }
        }
        private string NLSWebServiceUrl { get; set; }

    }
}
