﻿using AutoMapper;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Repository.Enums;
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
using JSSATSProject.Service.Models.PromotionRequestModel;
using JSSATSProject.Service.Models.SellOrderDetailsModel;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;
using Microsoft.AspNetCore.Http.Features.Authentication;






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

            CreateMap<Customer, ResponseCustomer>()
                    .ForMember(dest => dest.TotalPoint, opt => opt.MapFrom(src => src.Point != null ? src.Point.Totalpoint : 0))
                    .ForMember(dest => dest.AvaliablePoint, opt => opt.MapFrom(src => src.Point != null ? src.Point.AvailablePoint : 0))
                    .ForMember(dest => dest.SellOrders, opt => opt.MapFrom(src => src.SellOrders))
                    .ForMember(dest => dest.BuyOrders, opt => opt.MapFrom(src => src.BuyOrders))
                    .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
                    .ReverseMap();


            CreateMap<Customer, RequestUpdateCustomer>().ReverseMap();

            //Diamond
            CreateMap<Diamond, RequestCreateDiamond>().ReverseMap();
            CreateMap<Diamond, RequestUpdateDiamond>().ReverseMap();
            CreateMap<Diamond, RequestUpdateStatusDiamond>().ReverseMap();
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
            CreateMap<SellOrder, RequestCreateSellOrder>()
                .ForMember(dest => dest.SpecialDiscountRequestStatus, opt => opt.MapFrom(src => src.Status))
                .ReverseMap()
                .ForMember(dest => dest.SpecialDiscountRequest, opt => opt.Ignore());
            CreateMap<SellOrder, RequestUpdateSellOrder>().ReverseMap();
            CreateMap<SellOrder, UpdateSellOrderStatus>().ReverseMap();
            CreateMap<SellOrder, ResponseUpdateSellOrderWithSpecialPromotion>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => string.Join(" ", src.Customer.Firstname, src.Customer.Lastname)))
                .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.Customer.Phone))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.SpecialDiscountRate, opt => opt.MapFrom(src => src.SpecialDiscountRequest.DiscountRate))
                .ForMember(dest => dest.ProductCodesAndQuantity, opt => opt.MapFrom(src => src.SellOrderDetails.ToDictionary(s => s.Product.Code, s=> s.Quantity)))
                .ReverseMap();
            // CreateMap<SellOrder, ResponseOrder>().ReverseMap();
            CreateMap<SellOrder, RequestCreateSellOrder>().ReverseMap();
            CreateMap<SellOrder, RequestUpdateSellOrder>().ReverseMap();
            CreateMap<SellOrder, ResponseSellOrder>().ReverseMap();

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
            CreateMap<Product, RequestUpdateProduct>().ReverseMap();
            CreateMap<Product, ResponseProduct>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.DiamondCode,
                    opt => opt.MapFrom(src => src.ProductDiamonds.FirstOrDefault()!.Diamond.Code))
                .ForMember(dest => dest.DiamondName,
                    opt => opt.MapFrom(src => src.ProductDiamonds.FirstOrDefault()!.Diamond.Name))
                .ForMember(dest => dest.MaterialName,
                    opt => opt.MapFrom(src => src.ProductMaterials.FirstOrDefault()!.Material.Name))
                .ForMember(dest => dest.MaterialWeight,
                    opt => opt.MapFrom(src => src.ProductMaterials.FirstOrDefault()!.Weight))
                .ReverseMap()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.ProductDiamonds, opt => opt.Ignore())
                .ForMember(dest => dest.ProductMaterials, opt => opt.Ignore())
                ;

            //Promotion
            CreateMap<Promotion, RequestCreatePromotion>().ReverseMap();
            CreateMap<Promotion, RequestUpdatePromotion>().ReverseMap();
            CreateMap<Promotion, ResponsePromotion>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
                .ReverseMap();


            //ReturnBuyBackPolicy
            CreateMap<ReturnBuyBackPolicy, RequestCreateReturnBuyBackPolicy>().ReverseMap();
            CreateMap<ReturnBuyBackPolicy, ResponseReturnBuyBackPolicy>().ReverseMap();
            CreateMap<ReturnBuyBackPolicy, RequestUpdateReturnBuyBackPolicy>().ReverseMap();

            //Staff
            CreateMap<Staff, RequestCreateStaff>().ReverseMap();
            CreateMap<Staff, RequestUpdateStaff>().ReverseMap();
            // CreateMap<Staff, ResponseStaff>()
                 // .ForMember(dest => dest.TotalRevennue, opt => opt.Ignore())
                 // .ForMember(dest => dest.TotalOrder, opt => opt.Ignore())
                 // .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))
                 // .ReverseMap();
            CreateMap<Staff, ResponseStaff>()
                 .ForMember(dest => dest.TotalRevennue, opt => opt.Ignore())
                 .ForMember(dest => dest.TotalSellOrder, opt => opt.Ignore())
                 .ForMember(dest => dest.SellOrders, opt => opt.MapFrom(src => src.SellOrders))
                 .ReverseMap();


            //Stall
            CreateMap<Stall, RequestCreateStall>().ReverseMap();
            CreateMap<Stall, ResponseStall>().ReverseMap();

            //StallType
            CreateMap<StallType, RequestCreateStallType>().ReverseMap();
            CreateMap<StallType, ResponseStallType>().ReverseMap();

            //OrderDetail
            // CreateMap<OrderDetail, RequestCreateOrderDetail>().ReverseMap();
            // CreateMap<OrderDetail, RequestUpdateOrderDetail>().ReverseMap();
            // CreateMap<OrderDetail, ResponseOrderDetail>().ReverseMap();


            //LoginType
            CreateMap<Account, ResponseToken>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => string.Join(" ", src.Staff.Firstname, src.Staff.Lastname)))
                .ForMember(dest => dest.StaffId, opt => opt.MapFrom(src => src.Staff.Id))
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            //PromotionRequest
            CreateMap<PromotionRequest, ResponsePromotionRequest>()
                .ForMember(dest => dest.ApprovedByNavigation, opt => opt.MapFrom(src => src.ApprovedByNavigation))
                .ForMember(dest => dest.Manager, opt => opt.MapFrom(src => src.Manager))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
                .ReverseMap();

            CreateMap<PromotionRequest, CreatePromotionRequest>().ReverseMap();

            CreateMap<PromotionRequest, UpdatePromotionRequest>().ReverseMap();
            

            //SpecialDiscountRequest
            CreateMap<SpecialDiscountRequest, ResponseSpecialDiscountRequest>()
                .ForMember(dest => dest.ApprovedByNavigation, opt => opt.MapFrom(src => src.ApprovedByNavigation))
                .ForMember(dest => dest.Staff, opt => opt.MapFrom(src => src.Staff))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ReverseMap();
            CreateMap<SpecialDiscountRequest, CreateSpecialDiscountRequest>().ReverseMap();
            CreateMap<SpecialDiscountRequest, UpdateSpecialDiscountRequest>().ReverseMap();

            CreateMap<SellOrderDetail, RequestUpdateSellOrderDetailsStatus>().ReverseMap();
        }
    }
}
