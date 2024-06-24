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

        public string CreatePaymentUrl(RequestCreateVnPayment model, HttpContext context)
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
                pay.AddRequestData("vnp_Amount", ((long)model.Amount*100).ToString());
                pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
                pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
                pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
                pay.AddRequestData("vnp_OrderInfo", $"{model.OrderId} {model.Amount} {model.PaymentId} {model.PaymentMethodId}");
                pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
                pay.AddRequestData("vnp_TxnRef", tick);

                pay.AddRequestData("vnp_OrderType", "Buy");
                pay.AddRequestData("vnp_ExpireDate", "20250101103111");

                var paymentUrl = pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);
                return paymentUrl;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ResponseVnPayment PaymentExecute(IQueryCollection collections)
        {
            try
            {
                var pay = new VnPayLibrary();
                var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);


                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
