using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDSSNLSMPUtilsClasses;
namespace RDDSMakePayments
{
    public class FEUtils
    {

       
        public string FindLoan(string LoanNumber)
        {
            
            string sReturnValue = "";

            try
            {
                cNortridgeWapper nls = new cNortridgeWapper();
                cLoanInfo li = nls.GetLoanDetailInformation(LoanNumber);

                if (li == null)
                {
                    sReturnValue =  "Loan Not Found.  Check the loan number and try again.";
                    return sReturnValue;
                }
                else
                {
                    return  li.AccountName;
                }
                
            }
            catch (Exception ex)
            {
               return  "Error: " + ex.Message;
            }
            
        }

        cCustomerInfo GetCustomerInfo(string LoanNumber)
        {
            cCustomerInfo ci = new cCustomerInfo();
            cNortridgeWapper nls = new cNortridgeWapper();

            string strCustomerNumber = nls.GetCustomerNumberByLoan(LoanNumber);
            if (strCustomerNumber != "")
            {
                ci = nls.GetCustomerInformation(strCustomerNumber);
                if (ci != null)
                {
                    return ci;

                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
            
        }

    }
}
