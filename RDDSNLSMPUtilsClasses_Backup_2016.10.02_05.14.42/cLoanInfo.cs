using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RDSSNLSMPUtilsClasses
{
    public class cLoanInfo
    {
        public string LoanNumber { get; set; }
        public string AcctRefNo { get; set; }
        public string AccountName { get; set; }
        public string ShortName { get; set; }
        public string CurrentPayOffBalance { get; set; }
        public string CurrentDate { get; set; }
        public string CurrentMaturityDate { get; set; }
        public string InterestAccruedThruDate { get; set; }
        public string CurrentNoteAmount { get; set; }
        public string CurrentPrincipalBalance { get; set; }
        public string CurrentInterestBalance { get; set; }
        public string CurrentFeesBalance { get; set; }
        public string CurrentLateChargeBalance { get; set; }
        public string CurrentPerDiem { get; set; }
        public string CurrentInterestRate { get; set; }
        public string TotalPastDueBalance { get; set; }
        public string TotalCurrentDueBalance { get; set; }
        public string NextBillingDate { get; set; }
        public string DaysPastDue { get; set; }
        public string CurrentPending { get; set; }
        public string CurrentImpound { get; set; }
        public string LastPaymentDate { get; set; }
        public string LastPaymentAmount { get; set; }
        public string StatusCode { get; set; }
        public string LoanType { get; set; }
        public string OpenDate { get; set; }
        public string LastActivityDate { get; set; }
        public string CurrentUDF1Balance { get; set; }
        public string CurrentUDF2Balance { get; set; }
        public string CurrentUDF3Balance { get; set; }
        public string CurrentUDF4Balance { get; set; }
        public string CurrentUDF5Balance { get; set; }
        public string CurrentUDF6Balance { get; set; }
        public string CurrentUDF7Balance { get; set; }
        public string CurrentUDF8Balance { get; set; }
        public string CurrentUDF9Balance { get; set; }
        public string CurrentUDF10Balance { get; set; }
        public string CurrentSuspenseBalance { get; set; }
        public string InterestMethod { get; set; }
        public string TermChar { get; set; }
        public string TermDue { get; set; }
        public string Term { get; set; }
        public LoanDetail1UDF LoanDetail1UDF { get; set; }
        public LoanDetail2UDF LoanDetail2UDF { get; set; }

        public void Load(XmlDocument xdoc_loaninfo)
        {
            this.LoanNumber = xdoc_loaninfo.SelectSingleNode("//loan_number").InnerText;
            this.AccountName = xdoc_loaninfo.SelectSingleNode("//name").InnerText;
            this.AcctRefNo = xdoc_loaninfo.SelectSingleNode("//acctrefno").InnerText;
            this.ShortName = xdoc_loaninfo.SelectSingleNode("//shortname").InnerText;
            this.CurrentPayOffBalance = xdoc_loaninfo.SelectSingleNode("//current_payoff_balance").InnerText;
            this.CurrentDate = xdoc_loaninfo.SelectSingleNode("//curr_date").InnerText;
            this.CurrentMaturityDate = xdoc_loaninfo.SelectSingleNode("//curr_maturity_date").InnerText;
            this.InterestAccruedThruDate = xdoc_loaninfo.SelectSingleNode("//interest_accrued_thru_date").InnerText;
            this.CurrentNoteAmount = xdoc_loaninfo.SelectSingleNode("//current_note_amount").InnerText;
            this.CurrentPrincipalBalance = xdoc_loaninfo.SelectSingleNode("//current_principal_balance").InnerText;
            this.CurrentInterestBalance = xdoc_loaninfo.SelectSingleNode("//current_interest_balance").InnerText;
            this.CurrentFeesBalance = xdoc_loaninfo.SelectSingleNode("//current_fees_balance").InnerText;
            this.CurrentLateChargeBalance = xdoc_loaninfo.SelectSingleNode("//current_late_charge_balance").InnerText;
            this.CurrentPerDiem = xdoc_loaninfo.SelectSingleNode("//current_perdiem").InnerText;
            this.CurrentInterestRate = xdoc_loaninfo.SelectSingleNode("//current_interest_rate").InnerText;
            this.TotalPastDueBalance = xdoc_loaninfo.SelectSingleNode("//total_past_due_balance").InnerText;
            this.TotalCurrentDueBalance = xdoc_loaninfo.SelectSingleNode("//total_current_due_balance").InnerText;
            this.NextBillingDate = xdoc_loaninfo.SelectSingleNode("//next_billing_date").InnerText;
            this.DaysPastDue = xdoc_loaninfo.SelectSingleNode("//days_past_due").InnerText;
            this.CurrentPending = xdoc_loaninfo.SelectSingleNode("//current_pending").InnerText;
            this.CurrentImpound = xdoc_loaninfo.SelectSingleNode("//current_impound_balance").InnerText;
            this.LastPaymentDate = xdoc_loaninfo.SelectSingleNode("//last_payment_date").InnerText;
            this.LastPaymentAmount = xdoc_loaninfo.SelectSingleNode("//last_payment_amount").InnerText;
            this.StatusCode = xdoc_loaninfo.SelectSingleNode("//status_code").InnerText;
            this.LoanType = xdoc_loaninfo.SelectSingleNode("//loan_type").InnerText;
            this.OpenDate = xdoc_loaninfo.SelectSingleNode("//open_date").InnerText;
            this.LastActivityDate = xdoc_loaninfo.SelectSingleNode("//last_activity_date").InnerText;
            this.CurrentUDF1Balance = xdoc_loaninfo.SelectSingleNode("//current_udf1_balance").InnerText;
            this.CurrentUDF2Balance = xdoc_loaninfo.SelectSingleNode("//current_udf2_balance").InnerText;
            this.CurrentUDF3Balance = xdoc_loaninfo.SelectSingleNode("//current_udf3_balance").InnerText;
            this.CurrentUDF4Balance = xdoc_loaninfo.SelectSingleNode("//current_udf4_balance").InnerText;
            this.CurrentUDF5Balance = xdoc_loaninfo.SelectSingleNode("//current_udf5_balance").InnerText;
            this.CurrentUDF6Balance = xdoc_loaninfo.SelectSingleNode("//current_udf6_balance").InnerText;
            this.CurrentUDF7Balance = xdoc_loaninfo.SelectSingleNode("//current_udf7_balance").InnerText;
            this.CurrentUDF8Balance = xdoc_loaninfo.SelectSingleNode("//current_udf8_balance").InnerText;
            this.CurrentUDF9Balance = xdoc_loaninfo.SelectSingleNode("//current_udf9_balance").InnerText;
            this.CurrentUDF10Balance = xdoc_loaninfo.SelectSingleNode("//current_udf10_balance").InnerText;
            this.CurrentSuspenseBalance = xdoc_loaninfo.SelectSingleNode("//current_suspense_balance").InnerText;
            this.InterestMethod = xdoc_loaninfo.SelectSingleNode("//interest_method").InnerText;
            this.TermChar = xdoc_loaninfo.SelectSingleNode("//term_char").InnerText;
            this.Term = xdoc_loaninfo.SelectSingleNode("//term").InnerText;
            this.TermDue = xdoc_loaninfo.SelectSingleNode("//term_due").InnerText;
        }
    }
    public class LoanDetail1UDF
    {
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }
        public string udf6 { get; set; }
        public string udf7 { get; set; }
        public string udf8 { get; set; }
        public string udf9 { get; set; }
        public string udf10 { get; set; }
        public string udf11 { get; set; }
        public string udf12 { get; set; }
        public string udf13 { get; set; }
        public string udf14 { get; set; }
        public string udf15 { get; set; }
        public string udf16 { get; set; }
        public string udf17 { get; set; }
        public string udf18 { get; set; }
        public string udf19 { get; set; }
        public string udf20 { get; set; }
        public string udf21 { get; set; }
        public string udf22 { get; set; }
        public string udf23 { get; set; }
        public string udf24 { get; set; }
        public string udf25 { get; set; }
        public string udf26 { get; set; }
        public string udf27 { get; set; }
        public string udf28 { get; set; }
        public string udf29 { get; set; }
        public string udf30 { get; set; }
        public string udf31 { get; set; }
        public string udf32 { get; set; }
        public string udf33 { get; set; }
        public string udf34 { get; set; }
        public string udf35 { get; set; }
        public string udf36 { get; set; }
        public string udf37 { get; set; }
        public string udf38 { get; set; }
        public string udf39 { get; set; }
        public string udf40 { get; set; }
        public string udf41 { get; set; }
        public string udf42 { get; set; }
        public string udf43 { get; set; }
        public string udf44 { get; set; }
        public string udf45 { get; set; }
        public string udf46 { get; set; }
        public string udf47 { get; set; }
        public string udf48 { get; set; }
        public string udf49 { get; set; }
        public string udf50 { get; set; }

        public LoanDetail1UDF(string LoanUDF1XML)
        {
            XmlDocument xdoc_udfdetail1 = new XmlDocument();
            xdoc_udfdetail1.LoadXml(LoanUDF1XML);
            this.Load(xdoc_udfdetail1);

        }
        protected void Load(XmlDocument xdoc_loandetail1udf)
        {
            this.udf1 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF1").InnerText;
            this.udf2 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF2").InnerText;
            this.udf3 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF3").InnerText;
            this.udf4 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF4").InnerText;
            this.udf5 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF5").InnerText;
            this.udf6 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF6").InnerText;
            this.udf7 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF7").InnerText;
            this.udf8 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF8").InnerText;
            this.udf9 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF9").InnerText;
            this.udf10 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF10").InnerText;
            this.udf11 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF11").InnerText;
            this.udf12 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF12").InnerText;
            this.udf13 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF13").InnerText;
            this.udf14 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF14").InnerText;
            this.udf15 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF15").InnerText;
            this.udf16 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF16").InnerText;
            this.udf17 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF17").InnerText;
            this.udf18 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF18").InnerText;
            this.udf19 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF19").InnerText;
            this.udf20 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF20").InnerText;
            this.udf21 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF21").InnerText;
            this.udf22 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF22").InnerText;
            this.udf23 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF23").InnerText;
            this.udf24 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF24").InnerText;
            this.udf25 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF25").InnerText;
            this.udf26 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF26").InnerText;
            this.udf27 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF27").InnerText;
            this.udf28 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF28").InnerText;
            this.udf29 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF29").InnerText;
            this.udf30 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF30").InnerText;
            this.udf31 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF31").InnerText;
            this.udf32 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF32").InnerText;
            this.udf33 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF33").InnerText;
            this.udf34 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF34").InnerText;
            this.udf35 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF35").InnerText;
            this.udf36 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF36").InnerText;
            this.udf37 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF37").InnerText;
            this.udf38 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF38").InnerText;
            this.udf39 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF39").InnerText;
            this.udf40 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF40").InnerText;
            this.udf41 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF41").InnerText;
            this.udf42 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF42").InnerText;
            this.udf43 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF43").InnerText;
            this.udf44 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF44").InnerText;
            this.udf45 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF45").InnerText;
            this.udf46 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF46").InnerText;
            this.udf47 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF47").InnerText;
            this.udf48 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF48").InnerText;
            this.udf49 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF49").InnerText;
            this.udf50 = xdoc_loandetail1udf.SelectSingleNode("//LOAN_DETAIL1_UDF50").InnerText;
        }
    }
    public class LoanDetail2UDF
    {
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }
        public string udf6 { get; set; }
        public string udf7 { get; set; }
        public string udf8 { get; set; }
        public string udf9 { get; set; }
        public string udf10 { get; set; }
        public string udf11 { get; set; }
        public string udf12 { get; set; }
        public string udf13 { get; set; }
        public string udf14 { get; set; }
        public string udf15 { get; set; }
        public string udf16 { get; set; }
        public string udf17 { get; set; }
        public string udf18 { get; set; }
        public string udf19 { get; set; }
        public string udf20 { get; set; }
        public string udf21 { get; set; }
        public string udf22 { get; set; }
        public string udf23 { get; set; }
        public string udf24 { get; set; }
        public string udf25 { get; set; }
        public string udf26 { get; set; }
        public string udf27 { get; set; }
        public string udf28 { get; set; }
        public string udf29 { get; set; }
        public string udf30 { get; set; }
        public string udf31 { get; set; }
        public string udf32 { get; set; }
        public string udf33 { get; set; }
        public string udf34 { get; set; }
        public string udf35 { get; set; }
        public string udf36 { get; set; }
        public string udf37 { get; set; }
        public string udf38 { get; set; }
        public string udf39 { get; set; }
        public string udf40 { get; set; }
        public string udf41 { get; set; }
        public string udf42 { get; set; }
        public string udf43 { get; set; }
        public string udf44 { get; set; }
        public string udf45 { get; set; }
        public string udf46 { get; set; }
        public string udf47 { get; set; }
        public string udf48 { get; set; }
        public string udf49 { get; set; }
        public string udf50 { get; set; }

        public LoanDetail2UDF(string LoanUDF2XML)
        {
            XmlDocument xdoc_udfdetail2 = new XmlDocument();
            xdoc_udfdetail2.LoadXml(LoanUDF2XML);
            this.Load(xdoc_udfdetail2);

        }
        protected void Load(XmlDocument xdoc_loandetail2udf)
        {
            this.udf1 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF1").InnerText;
            this.udf2 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF2").InnerText;
            this.udf3 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF3").InnerText;
            this.udf4 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF4").InnerText;
            this.udf5 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF5").InnerText;
            this.udf6 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF6").InnerText;
            this.udf7 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF7").InnerText;
            this.udf8 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF8").InnerText;
            this.udf9 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF9").InnerText;
            this.udf10 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF10").InnerText;
            this.udf11 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF11").InnerText;
            this.udf12 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF12").InnerText;
            this.udf13 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF13").InnerText;
            this.udf14 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF14").InnerText;
            this.udf15 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF15").InnerText;
            this.udf16 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF16").InnerText;
            this.udf17 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF17").InnerText;
            this.udf18 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF18").InnerText;
            this.udf19 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF19").InnerText;
            this.udf20 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF20").InnerText;
            this.udf21 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF21").InnerText;
            this.udf22 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF22").InnerText;
            this.udf23 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF23").InnerText;
            this.udf24 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF24").InnerText;
            this.udf25 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF25").InnerText;
            this.udf26 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF26").InnerText;
            this.udf27 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF27").InnerText;
            this.udf28 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF28").InnerText;
            this.udf29 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF29").InnerText;
            this.udf30 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF30").InnerText;
            this.udf31 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF31").InnerText;
            this.udf32 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF32").InnerText;
            this.udf33 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF33").InnerText;
            this.udf34 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF34").InnerText;
            this.udf35 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF35").InnerText;
            this.udf36 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF36").InnerText;
            this.udf37 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF37").InnerText;
            this.udf38 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF38").InnerText;
            this.udf39 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF39").InnerText;
            this.udf40 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF40").InnerText;
            this.udf41 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF41").InnerText;
            this.udf42 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF42").InnerText;
            this.udf43 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF43").InnerText;
            this.udf44 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF44").InnerText;
            this.udf45 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF45").InnerText;
            this.udf46 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF46").InnerText;
            this.udf47 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF47").InnerText;
            this.udf48 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF48").InnerText;
            this.udf49 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF49").InnerText;
            this.udf50 = xdoc_loandetail2udf.SelectSingleNode("//LOAN_DETAIL2_UDF50").InnerText;
        }
    }
}
