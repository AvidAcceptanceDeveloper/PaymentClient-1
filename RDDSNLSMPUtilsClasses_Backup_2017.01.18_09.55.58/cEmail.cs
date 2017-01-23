using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Net.Mime;

namespace RDSSNLSMPUtilsClasses
{
    public class cEMail
    {
        bool invalid = false;
        public static byte[] ImageToByte2(Image img)
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Close();

                byteArray = stream.ToArray();
            }
            return byteArray;
        }
        public bool EmailCustomerReceipt(string sTo, string sFrom, string sSubject, string sBody)
        {
            cSettings oSettings = new cSettings(Properties.Settings.Default.SettingsFile);

            //var reader = ImageToByte2(Properties.Resources.emailFooter);
            //MemoryStream image1 = new MemoryStream(reader);
            //AlternateView av = AlternateView.CreateAlternateViewFromString(sBody, null, System.Net.Mime.MediaTypeNames.Text.Html);

            //LinkedResource headerImage = new LinkedResource(image1, System.Net.Mime.MediaTypeNames.Image.Jpeg);
            //headerImage.ContentId = "companyLogo";
            //headerImage.ContentType = new ContentType("image/jpg");
            //av.LinkedResources.Add(headerImage);


            string sHost = oSettings.smtphost;
            int iPort = int.Parse(oSettings.smtpport);
            string sUser = oSettings.smtpuserid;
            string sPassword = oSettings.smtppassword;

            System.Net.Mail.MailAddress oMailAddressTo = new System.Net.Mail.MailAddress(sTo);
            System.Net.Mail.MailAddress oMailAddressFrom = new MailAddress(oSettings.smtpuserid);
            System.Net.Mail.MailMessage oMailMessage = new MailMessage(oMailAddressFrom, oMailAddressTo);

            //oMailMessage.AlternateViews.Add(av);
            
            

            System.Net.Mail.SmtpClient oMailClient = new SmtpClient(sHost, iPort);
            ContentType mimeType = new System.Net.Mime.ContentType("text/html");
           // AlternateView alternate = AlternateView.CreateAlternateViewFromString(sBody, mimeType);
           // oMailMessage.AlternateViews.Add(alternate);

            System.Net.NetworkCredential netCred = new System.Net.NetworkCredential(sUser, sPassword);

            oMailClient.Credentials = netCred;

            oMailMessage.Subject = sSubject;
            oMailMessage.Body = sBody;
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
