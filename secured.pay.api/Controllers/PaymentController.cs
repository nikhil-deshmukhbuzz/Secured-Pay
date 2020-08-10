using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Razorpay.Api;
using secured.pay.api.Context;
using secured.pay.api.Models;

namespace secured.pay.api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        DB_Sec_Pay context = new DB_Sec_Pay();



        RazorpayClient client = null;

        [Route("REQ")]
        [HttpPost]
        public IActionResult REQ([FromBody] OrderRequest orderRequest)
        {
            try
            {
                var keyDetails = context.Razorpay_Configs
                     .Where(w => w.IsActive == true && w.Key == "KEY")
                     .FirstOrDefault();

                return Ok("success");
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                
            }
        }

        [Route("CreateOrder")]
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {
               
                var keyDetails = context.Razorpay_Configs
                    .Where(w => w.IsActive == true && w.Key == "KEY")
                    .FirstOrDefault();

                var secretDetails = context.Razorpay_Configs
                   .Where(w => w.IsActive == true && w.Key == "SECRET")
                   .FirstOrDefault();

                
                string key = keyDetails.Value;
                string secret = secretDetails.Value;
                int unit = keyDetails.Unit == 0 ? 1 : keyDetails.Unit;

                string receipt_id = "order_rcptid_"+ DateTime.Now.Ticks.ToString();

                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", orderRequest.Amount * unit); 
                options.Add("receipt", receipt_id);
                options.Add("currency", "INR");
                options.Add("payment_capture", "0");

                client = new RazorpayClient(key, secret);

                Order order = client.Order.Create(options);

                //internal db activity
                long TransactionStepID = 0;
                bool output = OrderCreation(order, orderRequest, out TransactionStepID);


                OrderResponse orderResponse = null;
                if (output)
                {
                    string  RedirectUrl = "http://pay.secured.web.s3-website.ap-south-1.amazonaws.com/payment.html?id=" + TransactionStepID;
                    // string RedirectUrl = "http://localhost:59549/payment.html?id="+ TransactionStepID;
                    var json = JsonConvert.SerializeObject(order.Attributes, Newtonsoft.Json.Formatting.Indented);
                    var attribute     = JsonConvert.DeserializeObject<RazorPay_Attribute>(json);

                    orderResponse = new OrderResponse()
                    {
                        RedirectUrl = RedirectUrl,
                        Status = "SUCCESS",
                        TransactionStepID = TransactionStepID
                    };
                }
                else
                {
                    orderResponse = new OrderResponse()
                    {
                        Status = "ERROR",
                    };
                }
                return Ok(orderResponse);
            }
            catch(Exception ex)
            {
                client = null;
                throw ex;
            }
            finally
            {
                client = null;
            }
        }

        [Route("GetOrder")]
        [HttpPost]
        public IActionResult GetOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {
                OrderResponse orderResponse = new OrderResponse();
                var keyDetails = context.Razorpay_Configs
                                   .Where(w => w.IsActive == true && w.Key == "KEY")
                                   .FirstOrDefault();

                var output = context.TransactionSteps
                .Include(i => i.RazorPay_Attribute)
                .Where(w => w.TransactionStepID == orderRequest.TransactionStepID)
                .FirstOrDefault();

                orderResponse.Key = keyDetails.Value;
                orderResponse.Response = output;

                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                context = null;
            }
        }

        [Route("Pay")]
        [HttpPost]
        public IActionResult Pay([FromBody] OrderRequest orderRequest)
        {
            try
            {
                var transactionStep = context.TransactionSteps
                .Include(i => i.RazorPay_Attribute)
                .Where(w => w.TransactionStepID == orderRequest.TransactionStepID)
                .FirstOrDefault();

                var transaction = new Transaction()
                {
                    PaymentID=orderRequest.PaymentID,
                    OrderID= orderRequest.OrderID,
                    Signature = orderRequest.Signature,
                    Amount = transactionStep.Amount,
                    PayeeName = transactionStep.PayeeName,
                    CustomerCode = transactionStep.CustomerCode,
                    ProductCode = transactionStep.ProductCode,
                    MobileNo = transactionStep.MobileNo,
                    Email = transactionStep.Email,
                    PaymentStatusID = (long)secured.pay.api.Enums.PaymentStatus.Success,
                    PaymentTypeID = (long)secured.pay.api.Enums.PaymentType.Online,
                    TransactionStepID = orderRequest.TransactionStepID,
      
              TransactionDate = DateTime.Now,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };

                bool output = AddTransaction(transaction);
                OrderResponse orderResponse = null;

                if (output)
                {
                    orderResponse = new OrderResponse()
                    {
                        Status = "SUCCESS"
                    };
                }
                else
                {
                    orderResponse = new OrderResponse()
                    {
                        Status = "ERROR"
                    };
                }

                orderResponse.Response = output;

                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                context = null;
            }
        }

        [Route("C_Pay")]
        [HttpPost]
        public IActionResult C_Pay([FromBody] OrderRequest orderRequest)
        {
            try
            {
                var transactionStep = context.TransactionSteps
                .Include(i => i.RazorPay_Attribute)
                .Where(w => w.TransactionStepID == orderRequest.TransactionStepID)
                .FirstOrDefault();

                var transaction = new C_Transaction()
                {
                    PaymentID = orderRequest.PaymentID,
                    OrderID = orderRequest.OrderID,
                    Signature = orderRequest.Signature,
                    Amount = transactionStep.Amount,
                    PayeeName = transactionStep.PayeeName,
                    CustomerCode = transactionStep.CustomerCode,
                    ProductCode = transactionStep.ProductCode,
                    MobileNo = transactionStep.MobileNo,
                    Email = transactionStep.Email,
                    PaymentStatusID = (long)secured.pay.api.Enums.PaymentStatus.Success,
                    PaymentTypeID = (long)secured.pay.api.Enums.PaymentType.Online,
                    TransactionStepID = orderRequest.TransactionStepID,

                    TransactionDate = DateTime.Now,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };

                bool output = Add_C_Transaction(transaction);
                OrderResponse orderResponse = null;

                if (output)
                {
                    orderResponse = new OrderResponse()
                    {
                        Status = "SUCCESS"
                    };
                }
                else
                {
                    orderResponse = new OrderResponse()
                    {
                        Status = "ERROR"
                    };
                }

                orderResponse.Response = output;

                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                context = null;
            }
        }


        [Route("GetLatestTransaction")]
        [HttpPost]
        public IActionResult GetLatestTransaction([FromBody] OrderRequest orderRequest)
        {
            TransactionResponse transactionResponse = null;
            try
            {
              
                var output = context.Transactions
                .Include(i => i.TransactionStep.Step)
                .Include(i => i.PaymentStatus)
                .Include(i => i.PaymentType)
                .Where(w => w.ProductCode == orderRequest.ProductCode && w.CustomerCode == orderRequest.CustomerCode)
                .OrderByDescending(o => o.TransactionDate)
                .FirstOrDefault();

                if(output != null)
                {
                    transactionResponse = new TransactionResponse();
                    transactionResponse.ProductCode = output.ProductCode;
                    transactionResponse.CustomerCode = output.CustomerCode;
                    transactionResponse.PaymentID = output.PaymentID;
                    transactionResponse.OrderID = output.OrderID;
                    transactionResponse.Signature = output.Signature;
                    transactionResponse.Amount = output.Amount;
                    transactionResponse.PayeeName = output.PayeeName;
                    transactionResponse.MobileNo = output.MobileNo;
                    transactionResponse.Email = output.Email;
                    transactionResponse.PaymentStatus = output.PaymentStatus.Status;
                    transactionResponse.PaymentType = output.PaymentType.Type;
                    transactionResponse.TransactionStep = output.TransactionStep.Step.Name;
                    transactionResponse.SuscriptionNumber = output.TransactionStep.SuscriptionNumber;
                    transactionResponse.TransactionDate = output.TransactionDate;
                    transactionResponse.CreatedOn = output.CreatedOn;
                    transactionResponse.ModifiedOn = output.ModifiedOn;
                }

                return Ok(transactionResponse);


            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                transactionResponse = null;
                context = null;
            }
        }

        [Route("GetTransactionByReceipt")]
        [HttpPost]
        public IActionResult GetTransactionByReceipt([FromBody] OrderRequest orderRequest)
        {
            TransactionResponse transactionResponse = null;
            try
            {

                var output = context.C_Transactions
                .Include(i => i.TransactionStep.Step)
                .Include(i => i.PaymentStatus)
                .Include(i => i.PaymentType)
                .Where(w => w.ProductCode == orderRequest.ProductCode && w.CustomerCode == orderRequest.CustomerCode && w.TransactionStep.InvoiceNumber == orderRequest.InvoiceNumber )
                .OrderByDescending(o => o.TransactionDate)
                .FirstOrDefault();

                if (output != null)
                {
                    transactionResponse = new TransactionResponse();
                    transactionResponse.ProductCode = output.ProductCode;
                    transactionResponse.CustomerCode = output.CustomerCode;
                    transactionResponse.PaymentID = output.PaymentID;
                    transactionResponse.OrderID = output.OrderID;
                    transactionResponse.Signature = output.Signature;
                    transactionResponse.Amount = output.Amount;
                    transactionResponse.PayeeName = output.PayeeName;
                    transactionResponse.MobileNo = output.MobileNo;
                    transactionResponse.Email = output.Email;
                    transactionResponse.PaymentStatus = output.PaymentStatus.Status;
                    transactionResponse.PaymentType = output.PaymentType.Type;
                    transactionResponse.TransactionStep = output.TransactionStep.Step.Name;
                    transactionResponse.InvoiceNumber = output.TransactionStep.InvoiceNumber;
                    transactionResponse.TransactionDate = output.TransactionDate;
                    transactionResponse.CreatedOn = output.CreatedOn;
                    transactionResponse.ModifiedOn = output.ModifiedOn;
                }

                return Ok(transactionResponse);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                transactionResponse = null;
                context = null;
            }
        }

        private bool OrderCreation(Order order,OrderRequest orderRequest,out long TransactionStepID)
        {
            bool result = false;
            TransactionStepID = 0;
            try
            {
                var transaction_step = new TransactionStep()
                {
                    PayeeName = orderRequest.PayeeName,
                    MobileNo = orderRequest.MobileNo,
                    Email = orderRequest.Email,
                    ProductCode = orderRequest.ProductCode,
                    CustomerCode = orderRequest.CustomerCode,
                    StepID = (long)secured.pay.api.Enums.Step.OrderCreated,
                    ApplicationUrl = orderRequest.ApplicationUrl,
                    SucessUrl = orderRequest.SucessUrl,
                    ErrorUrl = orderRequest.ErrorUrl,
                    Amount = orderRequest.Amount,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    SuscriptionNumber = orderRequest.SuscriptionNumber,
                    InvoiceNumber = orderRequest.InvoiceNumber
                };

                var json = JsonConvert.SerializeObject(order.Attributes, Newtonsoft.Json.Formatting.Indented);
                var attribute = JsonConvert.DeserializeObject<RazorPay_Attribute>(json);

                
                long razor_pay_output;
                using (var context = new DB_Sec_Pay())
                {
                    context.RazorPay_Attributes.Add(attribute);
                    razor_pay_output = context.SaveChanges();
                }

                transaction_step.RazorPay_AttributeID = attribute.RazorPay_AttributeID;


                long transaction_step_output;
                using (var context = new DB_Sec_Pay())
                {
                    context.TransactionSteps.Add(transaction_step);
                    transaction_step_output = context.SaveChanges();
                }

                TransactionStepID = transaction_step.TransactionStepID;

                result = true;
            }
            catch(Exception ex)
            {
                TransactionStepID = 0;
                result = false;
            }
            finally
            {
                context = null;
            }
            return result;
        }

        private bool AddTransaction(Transaction transaction)
        {
           bool result = false;
            try
            {
                long transaction_output;
                using (var context = new DB_Sec_Pay())
                {
                    context.Transactions.Add(transaction);
                    transaction_output = context.SaveChanges();
                }

                int output;
                using (var context = new DB_Sec_Pay())
                {
                    var input = context.TransactionSteps
                    .Where(w => w.TransactionStepID == transaction.TransactionStepID)
                    .FirstOrDefault();

                    input.StepID = (long)secured.pay.api.Enums.Step.PaymentCreated;
                    input.ModifiedOn = DateTime.Now;
                    output = context.SaveChanges();
                }
                result = true;
            }
            catch(Exception ex)
            {
                result = false;
            }
            finally
            {

            }
            return result;
        }

        private bool Add_C_Transaction(C_Transaction transaction)
        {
            bool result = false;
            try
            {
                long transaction_output;
                using (var context = new DB_Sec_Pay())
                {
                    context.C_Transactions.Add(transaction);
                    transaction_output = context.SaveChanges();
                }

                int output;
                using (var context = new DB_Sec_Pay())
                {
                    var input = context.TransactionSteps
                    .Where(w => w.TransactionStepID == transaction.TransactionStepID)
                    .FirstOrDefault();

                    input.StepID = (long)secured.pay.api.Enums.Step.PaymentCreated;
                    input.ModifiedOn = DateTime.Now;
                    output = context.SaveChanges();
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {

            }
            return result;
        }

        private string ComputeSha256Hash(string data)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}