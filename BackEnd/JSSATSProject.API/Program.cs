using System.Text;
using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.CacheManagers;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.AutoMapper;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace JSSATSProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            var services = builder.Services;

            services.AddHttpContextAccessor();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });
            builder.Services.AddAuthorization();

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

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefautConnection"));
            });


            builder.Services.AddScoped<UnitOfWork>();

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(ApplicationMapper));


            // Service
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IDiamondService, DiamondService>();
            //builder.Services.AddScoped<IDiamondPriceListService, DiamondPriceListService>();
            builder.Services.AddScoped<IGuaranteeService, GuaranteeService>();
            builder.Services.AddScoped<IMaterialPriceListService, MaterialPriceListService>();
            builder.Services.AddScoped<IMaterialService, MaterialService>();
            builder.Services.AddScoped<ISellOrderService, SellOrderService>();
            builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IPointService, PointService>();
            builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
            builder.Services.AddScoped<IProductCategoryTypeService, ProductCategoryTypeService>();
            //builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IPromotionService, PromotionService>();
            builder.Services.AddScoped<IReturnBuyBackPolicyService, ReturnBuyBackPolicyService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<IStallService, StallService>();
            builder.Services.AddScoped<IStallTypeService, StallTypeService>();
            builder.Services.AddScoped<IPromotionRequestService, PromotionRequestService>();
            builder.Services.AddScoped<ISpecialDiscountRequestService, SpecialDiscountRequestService>();
            //builder.Services.AddScoped<IVnPayService, VnPayService>();
            builder.Services.AddScoped<ISellOrderDetailService, SellOrderDetailService>();
            services.AddScoped<IPurchasePriceRatioService, PurchasePriceRatioService>();



            //CacheManager
            services.AddSingleton(typeof(CacheManager<>)); // Register generic CacheManager
            services.AddSingleton<DiamondPriceCacheManager>(); // Register cache for diamond prices
            services.AddScoped<IDiamondPriceListService, DiamondPriceListService>();
            services.AddScoped<IProductService, ProductService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Áp dụng middleware CORS
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            // CORS
            app.MapControllers();

            app.Run();
        }
    }
}