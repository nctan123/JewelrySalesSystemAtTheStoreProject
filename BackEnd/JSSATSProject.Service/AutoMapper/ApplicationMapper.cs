using AutoMapper;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models.AccountModel;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.Material;
using JSSATSProject.Service.Models.StaffModel;

using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Models.MaterialPriceListModel;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.PaymentMethodModel;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Models.PointModel;
using JSSATSProject.Service.Models.ProductCategoryModel;
using JSSATSProject.Service.Models.ProductCategoryTypeModel;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Models.PromotionModel;
using JSSATSProject.Service.Models.ReturnBuyBackPolicyModel;
using JSSATSProject.Service.Models.StallModel;
using JSSATSProject.Service.Models.StallTypeModel;
using JSSATSProject.Service.Models.DiamondModel;
using JSSATSProject.Service.Models.DiamondPriceListModel;






namespace JSSATSProject.Service.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            //Account
            CreateMap<Account, RequestSignUp>().ReverseMap();
            CreateMap<Account, RequestSignIn>().ReverseMap();
            CreateMap<Account, ResponseAccount>().ReverseMap();

            // Customer
            CreateMap<Customer, RequestCreateCustomer>().ReverseMap();
            CreateMap<Customer, ResponseCustomer>().ReverseMap();
            CreateMap<Customer, RequestUpdateCustomer>().ReverseMap();

            //Diamond
            CreateMap<Diamond, RequestCreateDiamond>().ReverseMap();
            CreateMap<Diamond, RequestUpdateDiamond>().ReverseMap();
            CreateMap<Diamond, ResponseDiamond>().ReverseMap();

            //DiamondPriceList
            CreateMap<DiamondPriceList, RequestCreateDiamondPriceList>().ReverseMap();
            CreateMap<DiamondPriceList, RequestUpdateDiamondPriceList>().ReverseMap();
            CreateMap<DiamondPriceList, ResponseDiamondPriceList>().ReverseMap();


            //Guarantee
            CreateMap<Guarantee, RequestCreateGuarantee>().ReverseMap();
            CreateMap<Guarantee, RequestUpdateGuarantee>().ReverseMap();
            CreateMap<Guarantee, ResponseGuarantee>().ReverseMap();


            //MaterialPrice
            CreateMap<MaterialPriceList, RequestCreateMaterialPriceList>().ReverseMap();
            CreateMap<MaterialPriceList, RequestUpdateMaterialPriceList>().ReverseMap();
            CreateMap<MaterialPriceList, ResponseMaterialPriceList>().ReverseMap();

            //Material
            CreateMap<Material, RequestCreateMaterial>().ReverseMap();
            CreateMap<Material, ResponseMaterial>().ReverseMap();

            //Order
            CreateMap<Order, RequestCreateOrder>().ReverseMap();
            CreateMap<Order, RequestUpdateOrder>().ReverseMap();
            CreateMap<Order, ResponseOrder>().ReverseMap();

            //PaymentMethod
            CreateMap<PaymentMethod, RequestCreatePaymentMethod>().ReverseMap();
            CreateMap<PaymentMethod, ResponsePaymentMethod>().ReverseMap();

            //Payment
            CreateMap<Payment, RequestCreatePayment>().ReverseMap();
            CreateMap<Payment, ResponsePayment>().ReverseMap();

            //Point
            CreateMap<Point, RequestCreatePoint>().ReverseMap();
            CreateMap<Point, ResponsePoint>().ReverseMap();

            //ProductCategory
            CreateMap<ProductCategory, RequestCreateProductCategory>().ReverseMap();
            CreateMap<ProductCategory, ResponseProductCategory>().ReverseMap();

            //ProductCategoryType
            CreateMap<ProductCategoryType, RequestCreateProductCategoryType>().ReverseMap();
            CreateMap<ProductCategoryType, ResponseProductCategoryType>().ReverseMap();

            //Product
            CreateMap<Product, RequestCreateProduct>().ReverseMap();
            CreateMap<Product, ResponseProduct>().ReverseMap();

            //Promotion
            CreateMap<Promotion, RequestCreatePromotion>().ReverseMap();
            CreateMap<Promotion, ResponsePromotion>().ReverseMap();

            //ReturnBuyBackPolicy
            CreateMap<ReturnBuyBackPolicy, RequestCreateReturnBuyBackPolicy>().ReverseMap();
            CreateMap<ReturnBuyBackPolicy, ResponseReturnBuyBackPolicy>().ReverseMap();

            //Staff
            CreateMap<Staff, RequestCreateStaff>().ReverseMap();
            CreateMap<Staff, RequestUpdateStaff>().ReverseMap();
            CreateMap<Staff, ResponseStaff>().ReverseMap();

            //Stall
            CreateMap<Stall, RequestCreateStall>().ReverseMap();
            CreateMap<Stall, ResponseStall>().ReverseMap();

            //StallType
            CreateMap<StallType, RequestCreateStallType>().ReverseMap();
            CreateMap<StallType, ResponseStallType>().ReverseMap();


        }
    }
}
