using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.AutoMapper;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JSSATSProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefautConnection"));
            });

            builder.Services.AddTransient<UnitOfWork>();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(ApplicationMapper));

            // Service
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IDiamondService, DiamondService>();
            builder.Services.AddScoped<IDiamondPriceListService, DiamondPriceListService>();
            builder.Services.AddScoped<IGuaranteeService, GuaranteeService>();
            builder.Services.AddScoped<IMaterialPriceListService, MaterialPriceListService>();
            builder.Services.AddScoped<IMaterialService, MaterialService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IPointService, PointService>();
            builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
            builder.Services.AddScoped<IProductCategoryTypeService, ProductCategoryTypeService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IPromotionService, PromotionService>();
            builder.Services.AddScoped<IReturnBuyBackPolicyService, ReturnBuyBackPolicyService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<IStallService, StallService>();

            // Cấu hình dịch vụ CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Áp dụng middleware CORS
            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}
