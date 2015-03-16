using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Globalization;

namespace RDSSNLSMPUtilsClasses
{
    public class cEMail
    {
        bool invalid = false;

        public bool EmailCustomerReceipt(string sTo, string sFrom, string sSubject, string sBody)
        {
            cSettings oSettings = new cSettings(Properties.Settings.Default.SettingsFile);

            System.Net.Mail.MailAddress oMailAddressTo = new System.Net.Mail.MailAddress(sTo);
            System.Net.Mail.MailAddress oMailAddressFrom = new MailAddress(sFrom);
            System.Net.Mail.MailMessage oMailMessage = new MailMessage(oMailAddressFrom, oMailAddressTo);
            oMailMessage.Body = sBody;

            string sHost = oSettings.smtphost;
            int iPort = int.Parse(oSettings.smtpport);
            string sUser = oSettings.smtpuserid;
            string sPassword = oSettings.smtppassword;

            System.Net.Mail.SmtpClient oMailClient = new SmtpClient(sHost, iPort);

            System.Net.NetworkCredential netCred = new System.Net.NetworkCredential(sUser, sPassword);

            oMailClient.Credentials = netCred;

            oMailClient.Send(oMailMessage);

            return true;
        }

        public bool EmailCSTransactionConfirmation()
        {
            return true;
        }

        public bool IsValidEmail(string strIn)
        {
            bool invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names. 
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format. 
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

        }
        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

    }
}
