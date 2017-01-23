using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace RDSSNLSMPUtilsClasses
{
    public class cCustomerInfo
    {
        private string _cifno, _cifnumber, _fname, _mname, _lname, _address1, _address2, _city, _state, _zip, _email;

        public void Load(XmlDocument xdoc_customer)
        {
            this.CifNo = xdoc_customer.SelectSingleNode("//cifno").InnerText;
            this.CifNumber = xdoc_customer.SelectSingleNode("//cifnumber").InnerText;
            this.FirstName = xdoc_customer.SelectSingleNode("//firstname1").InnerText;
            this.MiddleName = xdoc_customer.SelectSingleNode("//middlename1").InnerText;
            this.LastName = xdoc_customer.SelectSingleNode("//lastname1").InnerText;
            this.Address1 = xdoc_customer.SelectSingleNode("//street_address1").InnerText;
            this.Address2 = xdoc_customer.SelectSingleNode("//street_address2").InnerText;
            this.City = xdoc_customer.SelectSingleNode("//city").InnerText;
            this.State = xdoc_customer.SelectSingleNode("//state").InnerText;
            this.ZipCode = xdoc_customer.SelectSingleNode("//zip").InnerText;
            this.Email = xdoc_customer.SelectSingleNode("//email").InnerText;


        }

        public string CifNo
        {
            get { return _cifno; }
            set { _cifno = value; }
        }
        public string CifNumber
        {
            get { return _cifnumber; }
            set { _cifnumber = value; }
        }
        public string FirstName
        {
            get { return _fname; }
            set { _fname = value; }
        }
        public string MiddleName
        {
            get { return _mname; }
            set { _mname = value; }
        }
        public string LastName
        {
            get { return _lname; }
            set { _lname = value; }
        }
        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }
        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }
        public string ZipCode
        {
            get { return _zip; }
            set { _zip = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}
