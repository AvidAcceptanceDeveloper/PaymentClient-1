using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using ptOrbital = RDSSNLSMPUtilsClasses.net.paymentech.wsvar;
namespace RDSSNLSMPUtilsClasses
{
    public class cPaymentTech
    {
        public bool ServiceExists(
            string url,
            bool throwExceptions,
            out string errorMessage)
        {
            try
            {
                errorMessage = string.Empty;

                // try accessing the web service directly via it's URL
                HttpWebRequest request =
                    WebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 30000;

                using (HttpWebResponse response =
                           request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception("Error locating web service");
                }

                // try getting the WSDL?
                // asmx lets you put "?wsdl" to make sure the URL is a web service
                // could parse and validate WSDL here

            }
            catch (WebException ex)
            {
                // decompose 400- codes here if you like
                errorMessage =
                    string.Format("Error testing connection to web service at" +
                                  " \"{0}\":\r\n{1}", url, ex);
                Trace.TraceError(errorMessage);
                if (throwExceptions)
                    throw new Exception(errorMessage, ex);
            }
            catch (Exception ex)
            {
                errorMessage =
                    string.Format("Error testing connection to web service at " +
                                  "\"{0}\":\r\n{1}", url, ex);
                Trace.TraceError(errorMessage);
                if (throwExceptions)
                    throw new Exception(errorMessage, ex);
                return false;
            }

            return true;
        }

        public string ECMarkForCapture(string OrderId, string Amount, string ABARoutingNbr,
            string BankAccountNbr, string AccountType, string sEffectiveDte, string NameOnCheckingAccount,
            string Address, string City, string State, string Zip, string Address2 = "")
        {
            cSettings osettings = new cSettings(Properties.Settings.Default.SettingsFile);

            string sOrbitalUserName = osettings.OrbitalUserName;
            string sOrbitalPassword = osettings.OrbitalPassword;
            string sOrbitalApiUrl = osettings.OrbitalApiUrl;
            string sOrbitalApiUrl2 = osettings.OrbitalApiFailoverUrl;

            ptOrbital.PaymentechGateway ptorbital = new ptOrbital.PaymentechGateway();
            ptOrbital.NewOrderRequestElement nore = new ptOrbital.NewOrderRequestElement();

            nore.orbitalConnectionUsername = osettings.OrbitalUserName;
            nore.orbitalConnectionPassword = osettings.OrbitalPassword;

            nore.orderID = OrderId;
            nore.customerName = NameOnCheckingAccount;
            nore.transType = osettings.TransType;
            nore.bin = osettings.BIN;
            nore.merchantID = osettings.MerchantId;
            nore.terminalID = osettings.TerminalId;
            nore.amount = Amount.Replace(".", string.Empty).PadLeft(7, '0');
            nore.industryType = osettings.IndustryType;
            nore.ecpCheckRT = ABARoutingNbr;
            nore.ecpCheckDDA = BankAccountNbr;
            nore.ecpBankAcctType = AccountType;
            nore.cardBrand = "EC";
            nore.ecpDelvMethod = "B";
            //nore.useCustomerRefNum = OrderId;
            nore.ecpAuthMethod = "T";
            nore.avsName = NameOnCheckingAccount;
            nore.avsAddress1 = Address;
            nore.avsAddress2 = Address2;
            nore.avsCity = City;
            nore.avsState = State;
            nore.avsZip = Zip;

            string RetVal = "";
            //if (sEffectiveDte != "")
            //{
            //    RetVal = ECMarkForCaptureByProfile(OrderId, Amount, ABARoutingNbr,
            // BankAccountNbr, AccountType, sEffectiveDte, NameOnCheckingAccount,
            // Address, City, State, Zip, Address2);
            //    //nore.addProfileFromOrder = "O";
            //    //nore.profileOrderOverideInd = "OA";
            //    //nore.mbType = "R";

            //    //nore.mbRecurringFrequency = DateTime.Parse(sEffectiveDte).Day.ToString() + " * ?";
            //    //nore.mbOrderIdGenerationMethod = "DI";
            //    //nore.mbRecurringMaxBillings = "1";
            //    //nore.mbRecurringStartDate = DateTime.Parse(sEffectiveDte).ToString("MMddyyyy");
            //}


            
            ptOrbital.NewOrderResponseElement norespel = null;
            try
            {
                string ErrMsg = "";
                //check the service to make sure it is there, if not fail over
                if (ServiceExists(osettings.OrbitalApiUrl, false, out ErrMsg))
                {
                    ptorbital.Url = osettings.OrbitalApiUrl;
                }
                else
                {
                    ptorbital.Url = osettings.OrbitalApiFailoverUrl;
                }

                //ptorbital.Url = sOrbitalApiUrl;

                norespel = ptorbital.NewOrder(nore);
                if (norespel.procStatus == "0")
                {
                    if ((norespel.hostRespCode == "100") == true || (norespel.hostRespCode == "102") == true)
                        RetVal = OrderId + "|" + norespel.procStatusMessage + "|" + norespel.txRefNum + "|" + norespel.authorizationCode;
                    else
                        RetVal = "ERROR:  " + OrderId + " | " + norespel.procStatusMessage + " | Reference: " + norespel.txRefNum;
                }
                else
                {
                    RetVal = "ERROR:  " + OrderId + "|" + norespel.procStatusMessage + "|" + norespel.txRefNum;

                }

                return RetVal;
            }
            catch (Exception ex)
            {
                return "ERROR" + "|" + ex.Message + "|" + OrderId;
            }
        }

        public string CCMarkForCapture(string OrderId,
            string Amount, string ccAccountNum, string ccExpireDte, string ccSecCode, string NameOnCard,
            string Address, string City, string State, string Zip, string Address2 = "", string sEffectiveDte = "")
        {
            cSettings osettings = new cSettings(Properties.Settings.Default.SettingsFile);

            string sOrbitalUserName = osettings.OrbitalUserName;
            string sOrbitalPassword = osettings.OrbitalPassword;
            ptOrbital.PaymentechGateway ptorbital = new ptOrbital.PaymentechGateway();


            //ptorbital.Url = osettings.OrbitalApiUrl;

            ptOrbital.NewOrderRequestElement nore = new ptOrbital.NewOrderRequestElement();
            nore.orbitalConnectionUsername = osettings.OrbitalUserName;
            nore.orbitalConnectionPassword = osettings.OrbitalPassword;
            nore.orderID = OrderId;
            nore.transType = osettings.TransType;
            nore.industryType = osettings.IndustryType;
            nore.bin = osettings.BIN;
            nore.merchantID = osettings.MerchantId;
            nore.terminalID = osettings.TerminalId;
            nore.amount = Amount.PadLeft(7, '0');
            nore.industryType = osettings.IndustryType;
            nore.ccAccountNum = ccAccountNum;
            nore.ccExp = ccExpireDte;
            nore.avsName = NameOnCard;
            nore.avsAddress1 = Address;
            nore.avsAddress2 = Address2;
            nore.avsCity = City;
            nore.avsState = State;
            nore.avsZip = Zip;
            

            
            string RetVal = "";
            ptOrbital.NewOrderResponseElement norespel = null;
            try
            {
                string ErrMsg = "";
                //check the service to make sure it is there, if not fail over
                if (ServiceExists(osettings.OrbitalApiUrl, false, out ErrMsg))
                {
                    ptorbital.Url = osettings.OrbitalApiUrl;
                }
                else
                {
                    ptorbital.Url = osettings.OrbitalApiFailoverUrl;
                }


                norespel = ptorbital.NewOrder(nore);
                if (norespel.procStatus == "0")
                {
                    if ((norespel.hostRespCode == "100") == true || (norespel.hostRespCode == "102") == true)
                        RetVal = OrderId + "|" + norespel.procStatusMessage + "|" + norespel.txRefNum + "|" + norespel.authorizationCode;
                    else
                        RetVal = "ERROR:  " + norespel.procStatusMessage + "|" + OrderId + " |" + norespel.txRefNum;

                }
                else
                {
                    RetVal = "ERROR:  " + OrderId + " | " + norespel.procStatusMessage + "|" + norespel.txRefNum;
                }

                return RetVal;
            }
            catch (Exception ex)
            {
                return "ERROR" + "|" + ex.Message + "|" + OrderId;
            }



        }



        public string ECMarkForCaptureByProfile(string OrderId, string Amount, string ABARoutingNbr,
            string BankAccountNbr, string AccountType, string sEffectiveDte, string NameOnCheckingAccount,
            string Address, string City, string State, string Zip, string Address2 = "")
        {
            cSettings osettings = new cSettings(Properties.Settings.Default.SettingsFile);

            string sOrbitalUserName = osettings.OrbitalUserName;
            string sOrbitalPassword = osettings.OrbitalPassword;
            string sOrbitalApiUrl = osettings.OrbitalApiUrl;
            string sOrbitalApiUrl2 = osettings.OrbitalApiFailoverUrl;
            string ErrMsg = "";

            ptOrbital.PaymentechGateway ptorbital = new ptOrbital.PaymentechGateway();


            //ptorbital.Url = osettings.OrbitalApiUrl;

            ptOrbital.ProfileAddElement pae = new ptOrbital.ProfileAddElement();

            pae.orbitalConnectionUsername = osettings.OrbitalUserName;
            pae.orbitalConnectionPassword = osettings.OrbitalPassword;

            if (ServiceExists(osettings.OrbitalApiUrl, false, out ErrMsg))
            {
                ptorbital.Url = osettings.OrbitalApiUrl;
            }
            else
            {
                ptorbital.Url = osettings.OrbitalApiFailoverUrl;
            }


            
            pae.bin = osettings.BIN;
            pae.merchantID = osettings.MerchantId;
          
            pae.orderDefaultAmount = Amount.PadLeft(7, '0');
            pae.customerAccountType = "EC";
            pae.ecpCheckRT = ABARoutingNbr;
            pae.ecpCheckDDA = BankAccountNbr;
            pae.ecpBankAcctType = AccountType;
            pae.customerName = NameOnCheckingAccount;
            pae.customerAddress1 = Address;
            pae.customerAddress2 = Address2;
            pae.customerCity = City;
            pae.customerState = State;
            pae.customerZIP = Zip;
            
            pae.mbType = "R";

            pae.mbRecurringFrequency = DateTime.Parse(sEffectiveDte).Day.ToString() + " * ?";
            pae.mbOrderIdGenerationMethod = "DI";
            pae.mbRecurringMaxBillings = "1";
            pae.mbRecurringStartDate = DateTime.Parse(sEffectiveDte).ToString("MMddyyyy");
            pae.customerProfileFromOrderInd = "A";
            pae.customerProfileOrderOverideInd = "OA";
            
            ptOrbital.ProfileResponseElement prr = ptorbital.ProfileAdd(pae);

            if (prr.procStatus == "0")
                return prr.customerRefNum ;
            else
                return "ERROR";
            // }


        }
        public string CCMarkForCaptureByProfile(string OrderId,
            string Amount, string ccAccountNum, string ccExpireDte, string ccSecCode, string NameOnCard,
            string Address, string City, string State, string Zip, string Address2 = "", string sEffectiveDte = "")
        {
            cSettings osettings = new cSettings(Properties.Settings.Default.SettingsFile);

            string sOrbitalUserName = osettings.OrbitalUserName;
            string sOrbitalPassword = osettings.OrbitalPassword;
            string sOrbitalApiUrl = osettings.OrbitalApiUrl;
            string sOrbitalApiUrl2 = osettings.OrbitalApiFailoverUrl;
            string ErrMsg = "";

            ptOrbital.PaymentechGateway ptorbital = new ptOrbital.PaymentechGateway();


            //ptorbital.Url = osettings.OrbitalApiUrl;

            ptOrbital.ProfileAddElement pae = new ptOrbital.ProfileAddElement();

            pae.orbitalConnectionUsername = osettings.OrbitalUserName;
            pae.orbitalConnectionPassword = osettings.OrbitalPassword;

            if (ServiceExists(osettings.OrbitalApiUrl, false, out ErrMsg))
            {
                ptorbital.Url = osettings.OrbitalApiUrl;
            }
            else
            {
                ptorbital.Url = osettings.OrbitalApiFailoverUrl;
            }



            pae.bin = osettings.BIN;
            pae.merchantID = osettings.MerchantId;

            pae.orderDefaultAmount = Amount.PadLeft(7, '0');
            pae.customerAccountType = "CC";
            pae.ccAccountNum = ccAccountNum;
          
            pae.ccExp = ccExpireDte;
            pae.customerName = NameOnCard;
            pae.customerAddress1 = Address;
            pae.customerAddress2 = Address2;
            pae.customerCity = City;
            pae.customerState = State;
            pae.customerZIP = Zip;

            pae.mbType = "R";

            pae.mbRecurringFrequency = DateTime.Parse(sEffectiveDte).Day.ToString() + " * ?";
            pae.mbOrderIdGenerationMethod = "DI";
            pae.mbRecurringMaxBillings = "1";
            pae.mbRecurringStartDate = DateTime.Parse(sEffectiveDte).ToString("MMddyyyy");
            pae.customerProfileFromOrderInd = "A";
            pae.customerProfileOrderOverideInd = "OA";
            ptOrbital.ProfileResponseElement prr = null;
            try {
              prr = ptorbital.ProfileAdd(pae);
            }
            catch (Exception ex)
            {
                return "ERROR|" + ex.Message;
            }

            if (prr.procStatus == "0")
                return prr.customerRefNum;
            else
                return "ERROR";
            // }

        }
        public struct PaymentArguments
        {

        }
    } 
}
