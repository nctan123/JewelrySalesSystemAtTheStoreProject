using JSSATSProject.Repository.Entities;
using JSSATSProject.Repository;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace JSSATSProject.Service.Service.Service
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;

        public VnPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreatePaymentUrl(RequestCreatePayment model, HttpContext context)
        {
            try
            {
                var timeZoneId = _configuration["TimeZoneId"];
                if (string.IsNullOrEmpty(timeZoneId))
                    throw new ArgumentNullException("TimeZoneId is not configured.");

                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);

                var tick = DateTime.Now.Ticks.ToString();
                var pay = new VnPayLibrary();

                var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];
                if (string.IsNullOrEmpty(urlCallBack))
                    throw new ArgumentNullException("PaymentCallBack:ReturnUrl is not configured.");

                // Add required request data
                pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
                pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
                pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
                pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
                pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
                pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
                pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
                pay.AddRequestData("vnp_OrderInfo", $"{model.OrderId} {model.Amount}");
                pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
                pay.AddRequestData("vnp_TxnRef", tick);

                var paymentUrl = pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

                return paymentUrl;
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error creating payment URL.");
                throw;
            }
        }

        public ResponseVnPayment PaymentExecute(IQueryCollection collections)
        {
            try
            {
                var pay = new VnPayLibrary();
                var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

                //if (response.Success)
                //{
                //    // Use await to get the result from async methods
                //    var payment = await _unitOfWork.PaymentRepository.GetAsync(p => p.OrderId == order.Id);

                //    // Since GetAsync returns IEnumerable<Payment>, you can use LINQ methods after awaiting
                //    var existingPayment = payment.FirstOrDefault();

                //    if (existingPayment != null)
                //    {
                //        existingPayment.Status = "Paid";
                //        existingPayment.CreateDate = DateTime.UtcNow;
                //        await _unitOfWork.PaymentRepository.UpdateAsync(existingPayment);
                //        await _unitOfWork.SaveAsync();

                //        var paymentDetail = new PaymentDetail
                //        {
                //            PaymentId = existingPayment.Id,
                //            PaymentMethodId = 1, // Assuming 1 is the ID for VnPay
                //            Amount = existingPayment.Amount,
                //            ExternalTransactionCode = response.TransactionId,
                //            Status = "Success"
                //        };

                //        await _unitOfWork.PaymentDetailRepository.InsertAsync(paymentDetail);
                //        await _unitOfWork.SaveAsync();

                //        var sellOrder = await _unitOfWork.SellOrderRepository.GetAsync(o => o.Id == existingPayment.OrderId);
                //        var existingSellOrder = sellOrder.FirstOrDefault();
                //        if (existingSellOrder != null)
                //        {
                //            existingSellOrder.Status = "Paid";
                //            await _unitOfWork.SellOrderRepository.UpdateAsync(existingSellOrder);
                //            await _unitOfWork.SaveAsync();
                //        }
                //    }
                //}

                return response;
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error executing payment.");
                throw;
            }
        }
    }
}
