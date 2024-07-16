using System.Globalization;
using System.Text;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [Authorize(Roles = RoleConstants.Manager)]
    [ApiController]
    [Route("[controller]")]
    public class MetricsController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ISellOrderService _sellOrderService;
        private readonly ISellOrderDetailService _sellOrderDetailService;

        public MetricsController(
            ICustomerService customerService,
            ISellOrderService sellOrderService,
            ISellOrderDetailService sellOrderDetailService)
        {
            _customerService = customerService;
            _sellOrderService = sellOrderService;
            _sellOrderDetailService = sellOrderDetailService;
        }

        [HttpPost]
        [Route("csv/ExportChangeMetrics")]
        public async Task<IActionResult> CreateAsync(DateTime startDate, DateTime endDate)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Date,New Customers,Orders Number,Revenue");

            int totalNewCustomers = 0;
            int totalOrdersNumber = 0;
            decimal totalRevenue = 0;

            var allCustomers = (await _customerService.GetCustomersByDateRange(startDate, endDate));
            var allOrders = (await _sellOrderService.GetOrdersByDateRange(startDate, endDate));
            var allRevenue = (await _sellOrderService.GetTotalAmountByDateRange(startDate, endDate));

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (allCustomers.TryGetValue(date.Date, out int customerCount))
                {
                    totalNewCustomers += customerCount;
                } 
                if(allOrders.TryGetValue(date.Date, out int orderCount))
                {
                    totalOrdersNumber += orderCount;
                }
                if(allRevenue.TryGetValue(date.Date,out decimal totalAmount))
                {
                    totalRevenue += totalAmount;
                }

                sb.AppendLine($"{date:yyyy-MM-dd},{customerCount},{orderCount},{totalAmount.ToString(CultureInfo.InvariantCulture)}");
            }

            sb.AppendLine(
                $"Total,{totalNewCustomers},{totalOrdersNumber},{totalRevenue.ToString(CultureInfo.InvariantCulture)}");

            sb.AppendLine();
            sb.AppendLine("Category,Quantity");

            var productsSold =
                (List<Dictionary<string, object>>?)(await _sellOrderDetailService.CountProductsSoldByCategoryAsync(
                    startDate, endDate))
                .Data!;

            foreach (var product in productsSold)
            {
                sb.AppendLine($"{product["Category"]},{product["Quantity"]}");
            }

            var csvContent = sb.ToString();
            var fileName = $"metrics_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.csv";
            var bytes = Encoding.UTF8.GetBytes(csvContent);

            return File(bytes, "text/csv", fileName);
        }
    }
}