using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ptOrbital = RDSSNLSMPUtilsClasses.net.paymentech.wsvar;
namespace RDSSNLSMPUtilsClasses
{
    public class cPaymentTech
    {

        public string ECMarkForCapture(string OrderId, string Amount, string ABARoutingNbr, 
            string BankAccountNbr, string AccountType, string sEffectiveDte, string NameOnCheckingAccount, string Address, string City, string State, string Zip)
        {
            cSettings osettings = new cSettings(Properties.Settings.Default.SettingsFile);

            string sOrbitalUserName = osettings.OrbitalUserName;
            string sOrbitalPassword = osettings.OrbitalPassword;
            string sOrbitalApiUrl = osettings.OrbitalApiUrl;

            ptOrbital.PaymentechGateway ptorbital = new ptOrbital.PaymentechGateway();


            ptorbital.Url = sOrbitalApiUrl;
            ptOrbital.NewOrderRequestElement nore = new ptOrbital.NewOrderRequestElement();
            nore.orbitalConnectionUsername = osettings.OrbitalUserName;
            nore.orbitalConnectionPassword = osettings.OrbitalPassword;
            nore.orderID = OrderId;
            nore.customerName = NameOnCheckingAccount;
            nore.transType = osettings.TransType;
            nore.bin = osettings.BIN;
            nore.merchantID = osettings.MerchantId;
            nore.terminalID = osettings.TerminalId;
            nore.amount = Amount.Replace(".",string.Empty);
            nore.industryType = osettings.IndustryType;
            nore.ecpCheckRT = ABARoutingNbr;
            nore.ecpCheckDDA = BankAccountNbr;
            nore.ecpBankAcctType = AccountType;
            nore.cardBrand = "EC";
            nore.ecpDelvMethod = "A";

            nore.avsName = NameOnCheckingAccount;
            nore.avsAddress1 = Address;
            nore.avsCity = City;
            nore.avsState = State;
            nore.avsZip = Zip;

            if (sEffectiveDte !="")
            {
                nore.mbDeferredBillDate = sEffectiveDte;
            }


            string RetVal = "";
            try
            {

                ptOrbital.NewOrderResponseElement norespel = ptorbital.NewOrder(nore);
                if (norespel.procStatus == "0" )
                {
                    if ((norespel.hostRespCode == "100") == true || (norespel.hostRespCode == "102") == true)
                        RetVal = norespel.approvalStatus  + "|" + norespel.authorizationCode + "|" + norespel.procStatusMessage;
                    else
                        RetVal = "ERROR: Response Code = " + norespel.hostRespCode + " Status Message = " + norespel.procStatusMessage + " Response Message = " + norespel.respCodeMessage;
                }
                else
                {
                    RetVal = "ERROR: Status=" +   norespel.procStatus + "Message=" + norespel.procStatusMessage;

                }
                
                return RetVal;
            }
            catch (Exception ex)
            {
                return "ERROR" + "|" + ex.Message;
            }
        }

        public string CCMarkForCapture(string OrderId, 
            string Amount, string ccAccountNum, string ccExpireDte,string ccSecCode, string sEffectiveDte="")
        {
            cSettings osettings = new cSettings(Properties.Settings.Default.SettingsFile);
            
            string sOrbitalUserName = osettings.OrbitalUserName;
            string sOrbitalPassword = osettings.OrbitalPassword;
            ptOrbital.PaymentechGateway ptorbital = new ptOrbital.PaymentechGateway();
            

            ptorbital.Url = osettings.OrbitalApiUrl;

            ptOrbital.NewOrderRequestElement nore = new ptOrbital.NewOrderRequestElement();
                nore.orbitalConnectionUsername = osettings.OrbitalUserName;
                nore.orbitalConnectionPassword = osettings.OrbitalPassword;
                nore.orderID = OrderId;
                nore.transType = osettings.TransType;
                nore.bin = osettings.BIN;
                nore.merchantID = osettings.MerchantId;
                nore.terminalID = osettings.TerminalId;
                nore.amount = Amount;
                nore.industryType = osettings.IndustryType;
                nore.ccAccountNum = ccAccountNum;
                nore.ccExp = ccExpireDte;
                //nore.ccCardVerifyNum = ccSecCode;
                //nore.ccCardVerifyPresenceInd = null;

            if (sEffectiveDte != "")
            {
                nore.mbDeferredBillDate = sEffectiveDte;
            }
            string RetVal = "";
            try
            {

                ptOrbital.NewOrderResponseElement norespel = ptorbital.NewOrder(nore);
                if (norespel.procStatus == "0")
                {
                    if ((norespel.hostRespCode == "100") == true || (norespel.hostRespCode == "102") == true)
                        RetVal = norespel.approvalStatus + "|" + norespel.authorizationCode + "|" + norespel.procStatusMessage;
                    else
                        RetVal = "ERROR: Response Code = " + norespel.hostRespCode + " Status Message = " + norespel.procStatusMessage + " Response Message = " + norespel.respCodeMessage;
                }
                else
                {
                    RetVal = "ERROR: Status=" + norespel.procStatus + "Message=" + norespel.procStatusMessage;

                }

                return RetVal;
            }
            catch (Exception ex)
            {
                return "ERROR" + "|" + ex.Message;
            }


           
        }
    }
    public struct PaymentArguments
    {

    }
}
