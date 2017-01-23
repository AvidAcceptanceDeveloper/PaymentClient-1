using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;

namespace RDSSNLSMPUtilsClasses
{
    /// <summary>
    /// Test code written by Authorize.net
    /// </summary>
    public class cAuthNet
    {
        private cSettings Settings { get; set; }

        public cAuthNet()
        {
            Settings = new cSettings(Properties.Settings.Default.SettingsFile);
        }
        public string[] ConvertResponseToString(ANetApiResponse oResponse)
        {
            string[] UnpackedResponse = null;
            UnpackedResponse[0] = oResponse.refId;
            UnpackedResponse[1] = oResponse.messages.resultCode.ToString();
            return UnpackedResponse;

        }
        public string AuthNetPayByACH(String ApiLoginID, String ApiTransactionKey,  string FirstName, string LastName,string RoutingNumber, string BankAcctNumber, string PaymentAmount,string payeremail="")
        {
            string responsestring = "";

            //Console.WriteLine("Authorize Credit Card Sample");
            if (this.Settings.TestMode == "test")
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            else
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            

            var bankAccount = new bankAccountType
            {
                accountNumber = BankAcctNumber,
                routingNumber = RoutingNumber,
                echeckType = echeckTypeEnum.TEL,   // change based on how you take the payment (web, telephone, etc)
                nameOnAccount = FirstName + " " + LastName
            };

            customerAddressType cat = new customerAddressType()
            {
                firstName = FirstName,
                lastName = LastName
            };

            var paymentType = new paymentType { Item = bankAccount };
            //standard api call to retrieve response
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authOnlyTransaction.ToString(),    // authorize only
                amount = decimal.Parse(PaymentAmount.Replace("$", string.Empty)),
                payment = paymentType,
                 
            };
            
            var customer = new customerDataType
            {
                email = payeremail
            };
           

            transactionRequest.customer = customer;
            transactionRequest.billTo = cat;
            

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();
            if (response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.transactionResponse != null)
                {
                    responsestring = "Ok" + ":" + response.transactionResponse.transId;
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                if (response.transactionResponse != null)
                {
                    responsestring = "Error" + ":" + response.transactionResponse.errors[0].errorCode + ":" + response.transactionResponse.errors[0].errorText;
                }
            }
            return responsestring;
        }
        public string AuthNetPayByCCDebit(String ApiLoginID, String ApiTransactionKey, string FirstName, string LastName, string cardNumber, string ExpireDate, string SecurityCode, string PaymentAmount, string EffectiveDate = "", string payeremail="")
        {
            string responsestring = "";
            PaymentAmount = PaymentAmount.Replace("$", string.Empty);

            //Console.WriteLine("Authorize Credit Card Sample");
            if (this.Settings.TestMode == "test")
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            else
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                
                cardNumber = cardNumber,
                expirationDate = ExpireDate,
                cardCode = SecurityCode
                
            };

            customerAddressType cat = new customerAddressType()
            {
                firstName = FirstName,
                lastName = LastName
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),   // charge the card
                amount = decimal.Parse(PaymentAmount),
                payment = paymentType

            };
            
            var customer = new customerDataType()
            {
                email = payeremail
            };
            transactionRequest.customer = customer;
            transactionRequest.billTo = cat;

            nameAndAddressType addressInfo = new nameAndAddressType()
            {
                firstName = FirstName,
                lastName = LastName
            };
            
            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            if (response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.transactionResponse != null)
                {
                    responsestring = "Ok" + ":" + response.transactionResponse.transId;
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                if (response.transactionResponse != null)
                {
                    responsestring = "Error" + ":" + response.transactionResponse.errors[0].errorCode + ":" + response.transactionResponse.errors[0].errorText;
                }
            }
            return responsestring;
        }
        public string AuthNetPostDatePaymentCCDebit(String ApiLoginID, String ApiTransactionKey, string fName, string lName, decimal PaymentAmount, int IntervalLength, short TotalOccurances, DateTime startdate, string CCDbtCardNumber,
            string ExpireDate, string SecCode, string payeremail="")
        {

            string responsestring = "";

            if (this.Settings.TestMode == "test")
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            else
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();

            interval.length = 1;                        // months can be indicated between 1 and 12
            interval.unit = ARBSubscriptionUnitEnum.months;

            paymentScheduleType schedule = new paymentScheduleType
            {
                interval = interval,
                startDate = startdate,      // start date should be tomorrow
                totalOccurrences = TotalOccurances,                          // 999 indicates no end date
                trialOccurrences = 0
            };

            #region Payment Information
            var creditCard = new creditCardType
            {
                cardNumber = CCDbtCardNumber,
                expirationDate = ExpireDate,
                cardCode = SecCode
            };

            //standard api call to retrieve response
            paymentType cc = new paymentType { Item = creditCard };
            #endregion
            customerDataType customer = new customerDataType()
            {
                email = payeremail
            };
           
            nameAndAddressType addressInfo = new nameAndAddressType()
            {
                firstName = fName,
                lastName = lName
            };

            ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
            {
                amount = PaymentAmount,
                trialAmount = 0.00m,
                paymentSchedule = schedule,
                billTo = addressInfo,
                payment = cc
            };

            var request = new ARBCreateSubscriptionRequest { subscription = subscriptionType };

            var controller = new ARBCreateSubscriptionController(request);          // instantiate the contoller that will call the service
            controller.Execute();

            ARBCreateSubscriptionResponse response = controller.GetApiResponse();  // get the response from the service (errors contained if any)

            // get the response from the service (errors contained if any)
            if (response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.subscriptionId != null)
                {
                    responsestring = "Ok" + ":" + response.subscriptionId;
                }
            }
            else
            {


                responsestring = "Error" + ":" + response.messages.message[0].code + "  " + response.messages.message[0].text;

            }
            return responsestring;

        }
        public string AuthNetPostDatePaymentACH(String ApiLoginID, String ApiTransactionKey, string fName, string lName, decimal PaymentAmount,
            int IntervalLength, short TotalOccurances, DateTime startdate, string BankAcctNumber, string RoutingNumber, string payeremail="")
        {
            string responsestring = "";

            if (this.Settings.TestMode == "test")
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            else
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();

            interval.length = 1;                        // months can be indicated between 1 and 12
            interval.unit = ARBSubscriptionUnitEnum.months;

            paymentScheduleType schedule = new paymentScheduleType
            {
                interval = interval,
                startDate = startdate,      // start date should be tomorrow
                totalOccurrences = TotalOccurances,                          // 999 indicates no end date
                trialOccurrences = 0
            };

            #region Payment Information
            var bankAccount = new bankAccountType
              {
                  accountNumber = BankAcctNumber,
                  routingNumber = RoutingNumber,
                  echeckType = echeckTypeEnum.PPD,   // change based on how you take the payment (web, telephone, etc)
                  nameOnAccount = fName + " " + lName
              };


            var paymentType = new paymentType { Item = bankAccount };

            //standard api call to retrieve response

            #endregion
            var customer = new customerDataType
            {
                email = payeremail
            };
            nameAndAddressType addressInfo = new nameAndAddressType()
            {
                firstName = fName,
                lastName = lName
            };

            ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
            {
                amount = PaymentAmount,
                trialAmount = 0.00m,
                paymentSchedule = schedule,
                billTo = addressInfo,
                payment = paymentType
            };

            var request = new ARBCreateSubscriptionRequest { subscription = subscriptionType };

            var controller = new ARBCreateSubscriptionController(request);          // instantiate the contoller that will call the service
            controller.Execute();

            ARBCreateSubscriptionResponse response = controller.GetApiResponse();  // get the response from the service (errors contained if any)

            // get the response from the service (errors contained if any)
            if (response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.subscriptionId != null)
                {
                    responsestring = "Ok" + ":" + response.subscriptionId;
                }
            }
            else
            {

                responsestring = "Error" + ":" + response.messages.message[0].code + "  " + response.messages.message[0].text;

            }
            return responsestring;
        }

    }
    
}
