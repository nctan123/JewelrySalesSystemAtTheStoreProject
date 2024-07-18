using AutoMapper;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models._4CModel;
using JSSATSProject.Service.Models.AccountModel;
using JSSATSProject.Service.Models.BuyOrderDetailModel;
using JSSATSProject.Service.Models.BuyOrderModel;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.DiamondModel;
using JSSATSProject.Service.Models.DiamondPriceListModel;
using JSSATSProject.Service.Models.FluorescenceModel;
using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Models.Material;
using JSSATSProject.Service.Models.MaterialPriceListModel;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.OriginModel;
using JSSATSProject.Service.Models.PaymentDetailModel;
using JSSATSProject.Service.Models.PaymentMethodModel;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Models.PointModel;
using JSSATSProject.Service.Models.PolishModel;
using JSSATSProject.Service.Models.ProductCategoryModel;
using JSSATSProject.Service.Models.ProductCategoryTypeModel;
using JSSATSProject.Service.Models.ProductDiamondModel;
using JSSATSProject.Service.Models.ProductMaterialModel;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Models.PromotionModel;
using JSSATSProject.Service.Models.PromotionRequestModel;
using JSSATSProject.Service.Models.PurchasePriceRatioModel;
using JSSATSProject.Service.Models.ReturnBuyBackPolicyModel;
using JSSATSProject.Service.Models.SellOrderDetailsModel;
using JSSATSProject.Service.Models.SellOrderModel;
using JSSATSProject.Service.Models.ShapeModel;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;
using JSSATSProject.Service.Models.StaffModel;
using JSSATSProject.Service.Models.StallModel;
using JSSATSProject.Service.Models.StallTypeModel;
using JSSATSProject.Service.Models.SymmetryModel;

namespace JSSATSProject.Service.AutoMapper;

public class ApplicationMapper : Profile
{
    public ApplicationMapper()
    {
        //Account
        CreateMap<Account, ResponseAccount>()
            .ForMember(dest => dest.StaffName,
                opt => opt.MapFrom(src => src.Staff.Firstname + " " + src.Staff.Lastname))
            .ReverseMap();
        CreateMap<Account, ResponseAccount>()
            .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff.Firstname + " " + src.Staff.Lastname))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
            .ReverseMap();

        //BuyOrder
        CreateMap<BuyOrder, ResponseBuyOrder>()
            .ForMember(dest => dest.StaffName,
                opt => opt.MapFrom(src => string.Join(" ", src.Staff.Firstname, src.Staff.Lastname)))
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => string.Join(" ", src.Customer.Firstname, src.Customer.Lastname)))
            .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.Customer.Phone))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.Payments
                                                                                .SelectMany(p => p.PaymentDetails)
                                                                                .Select(pd => pd.PaymentMethod.Name)
                                                                                 .FirstOrDefault()))
            .ReverseMap()
            .ForMember(dest => dest.BuyOrderDetails, opt => opt.Ignore())
            ;

        //BuyOrderDetail
        CreateMap<BuyOrderDetail, ResponseBuyOrderDetail>()
            .ForMember(dest => dest.CategoryType, opt => opt.MapFrom(src => src.CategoryType.Name))
            .ForMember(dest => dest.CaregoryId, opt => opt.MapFrom(src => src.CategoryType.Id))
            .ForMember(dest => dest.MaterialName, opt => opt.MapFrom(src => src.Material.Name))
            .ForMember(dest => dest.PurchasePriceRatio, opt => opt.MapFrom(src => src.PurchasePriceRatio.Percentage))
            .ReverseMap();

        // Customer
        CreateMap<Customer, RequestCreateCustomer>().ReverseMap();
        CreateMap<Customer, ResponseCustomer>()
            //.ForMember(dest => dest.TotalPoint, opt => opt.MapFrom(src => src.Point != null ? src.Point.Totalpoint : 0))
            //.ForMember(dest => dest.AvaliablePoint, opt => opt.MapFrom(src => src.Point != null ? src.Point.AvailablePoint : 0))
            //.ForMember(dest => dest.SellOrders, opt => opt.MapFrom(src => src.SellOrders))
            //.ForMember(dest => dest.BuyOrders, opt => opt.MapFrom(src => src.BuyOrders))
            .ReverseMap();


        CreateMap<Customer, RequestUpdateCustomer>().ReverseMap();

        //Diamond
        CreateMap<RequestCreateDiamond, Diamond>()
                   .ForMember(dest => dest.Code, opt => opt.Ignore());

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

        //SellOrder
        CreateMap<SellOrder, RequestCreateSellOrder>()
            .ForMember(dest => dest.SpecialDiscountRequestStatus, opt => opt.MapFrom(src => src.Status))
            .ReverseMap()
            .ForMember(dest => dest.SpecialDiscountRequest, opt => opt.Ignore());

        CreateMap<SellOrder, RequestUpdateSellOrder>().ReverseMap();

        CreateMap<SellOrder, UpdateSellOrderStatus>().ReverseMap();

        CreateMap<SellOrder, ResponseUpdateSellOrderWithSpecialPromotion>()
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => string.Join(" ", src.Customer.Firstname, src.Customer.Lastname)))
            .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.Customer.Phone))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.SpecialDiscountRate,
                opt => opt.MapFrom(src => src.SpecialDiscountRequest.DiscountRate))
            .ForMember(dest => dest.ProductCodesAndQuantity,
                opt => opt.MapFrom(src => src.SellOrderDetails.ToDictionary(s => s.Product.Code, s => s.Quantity)))
            .ReverseMap();

        CreateMap<SellOrder, ResponseSellOrder>()
            .ForMember(dest => dest.SellOrderDetails, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerName,opt => opt.MapFrom(src => string.Join(" ", src.Customer.Firstname, src.Customer.Lastname)))
            .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.Customer.Phone))
             .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
            .ForMember(dest => dest.StaffName,opt => opt.MapFrom(src => string.Join(" ", src.Staff.Firstname, src.Staff.Lastname)))
            .ForMember(dest => dest.SpecialDiscountRate,opt => opt.MapFrom(src => src.SpecialDiscountRequest.DiscountRate))
            .ForMember(dest => dest.SpecialDiscountStatus, opt => opt.MapFrom(src => src.SpecialDiscountRequest.Status))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.Payments
                                                                                .SelectMany(p => p.PaymentDetails)
                                                                                .Select(pd => pd.PaymentMethod.Name)
                                                                                 .FirstOrDefault()))
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Payments
                                                                     .Select(p => p.Id)
                                                                     .FirstOrDefault()))
            .ReverseMap();

        CreateMap<SellOrderDetail, ResponseSellOrderDetails>()
            .ForMember(dest => dest.ProductId,opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName,opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.Product.Code))
            .ForMember(dest => dest.PromotionRate, opt =>opt.MapFrom(src => src.Promotion.DiscountRate))
            .ForMember(dest => dest.Img,opt => opt.MapFrom(src => src.Product.Img))
            .ForMember(dest =>dest.GuaranteeCode, opt => opt.MapFrom(src => src.Product.Guarantees.FirstOrDefault().Code))
            .ReverseMap();


        //PaymentMethod
        CreateMap<PaymentMethod, RequestCreatePaymentMethod>().ReverseMap();
        CreateMap<PaymentMethod, ResponsePaymentMethod>().ReverseMap();

        //Payment
        CreateMap<Payment, ResponsePayment>()
                .ForMember(dest => dest.ExternalTransactionCode, opt => opt.MapFrom(src => src.PaymentDetails.FirstOrDefault().ExternalTransactionCode))
                .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentDetails.FirstOrDefault().PaymentMethod.Name))
                .ForMember(dest => dest.SellOrderCode, opt => opt.MapFrom(src => src.Sellorder.Code))
                .ForMember(dest => dest.BuyOrderCode,opt => opt.MapFrom(src => src.Buyorder.Code))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => string.Join(" ", src.Customer.Firstname, src.Customer.Lastname)))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Customer.Phone))
                .ForMember(dest => dest.CustomerId,opt => opt.MapFrom(src => src.Customer.Id))
                .ReverseMap();
        CreateMap<Payment, RequestCreatePayment>().ReverseMap();

        //Point
        CreateMap<Point, RequestCreatePoint>().ReverseMap();
        CreateMap<Point, ResponsePoint>().ReverseMap();
        CreateMap<Point, RequestUpdatePoint>().ReverseMap();

        //ProductCategory
        CreateMap<ProductCategory, RequestCreateProductCategory>().ReverseMap();
        CreateMap<ProductCategory, ResponseProductCategory>().ReverseMap();

        //ProductCategoryType
        CreateMap<ProductCategoryType, RequestCreateProductCategoryType>().ReverseMap();
        CreateMap<ProductCategoryType, ResponseProductCategoryType>().ReverseMap();

        //Product
        CreateMap<Product, RequestCreateProduct>().ReverseMap();

        CreateMap<Product, RequestUpdateProduct>()
            .ForMember(dest => dest.StallsId, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.StallsId, opt => opt.MapFrom(src => src.StallsId));

        CreateMap<Product, ResponseProduct>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
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
            .ForMember(dest => dest.ProductMaterials, opt => opt.Ignore());

        CreateMap<Product, ResponseProductForCheckOrder>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();


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
        CreateMap<Staff, ResponseStaff>()
            .ForMember(dest => dest.TotalRevenue, opt => opt.Ignore())
            .ForMember(dest => dest.TotalSellOrder, opt => opt.Ignore())
            .ReverseMap();


        //Stall
        CreateMap<Stall, RequestCreateStall>().ReverseMap();
        CreateMap<Stall, ResponseStall>().ReverseMap();

        //StallType
        CreateMap<StallType, RequestCreateStallType>().ReverseMap();
        CreateMap<StallType, ResponseStallType>().ReverseMap();


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
            .ForMember(dest => dest.ManagerName,
                opt => opt.MapFrom(src => $"{src.Manager.Firstname} {src.Manager.Lastname}"))
            .ForMember(dest => dest.ApprovedName,
                opt => opt.MapFrom(src => $"{src.ApprovedByNavigation.Firstname} {src.ApprovedByNavigation.Lastname}"))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            .ReverseMap();

        CreateMap<PromotionRequest, CreatePromotionRequest>().ReverseMap();
        CreateMap<PromotionRequest, UpdatePromotionRequest>().ReverseMap();


        //SpecialDiscountRequest
        CreateMap<SpecialDiscountRequest, ResponseSpecialDiscountRequest>()
            .ForMember(dest => dest.ApprovedName,
                opt => opt.MapFrom(src => $"{src.ApprovedByNavigation.Firstname} {src.ApprovedByNavigation.Lastname}"))
            .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => $"{src.Staff.Firstname} {src.Staff.Lastname}"))
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => $"{src.Customer.Firstname} {src.Customer.Lastname}"))
            .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Customer.Phone))
            .ForMember(dest => dest.SellOrderId, opt => opt.MapFrom(src => src.SellOrders.FirstOrDefault().Id))
             .ForMember(dest => dest.SellOrderCode, opt => opt.MapFrom(src => src.SellOrders.FirstOrDefault().Code))
            .ReverseMap();


        CreateMap<SpecialDiscountRequest, CreateSpecialDiscountRequest>().ReverseMap();
        CreateMap<SpecialDiscountRequest, UpdateSpecialDiscountRequest>().ReverseMap();
        CreateMap<SellOrderDetail, RequestUpdateSellOrderDetailsStatus>().ReverseMap();
        CreateMap<PaymentDetail, RequestCreatePaymentDetail>().ReverseMap();
        CreateMap<PaymentDetail, ResponsePaymentDetail>().ReverseMap();

        //PurchasePriceRatio
        CreateMap<PurchasePriceRatio, RequestCreatePurchasePriceRatio>().ReverseMap();
        CreateMap<PurchasePriceRatio, ResponsePurchasePriceRatio>()
            .ForMember(dest => dest.CategoryTypeName, opt => opt.MapFrom(src => src.CategoryType.Name))
            .ReverseMap();

        //4C
        CreateMap<Carat, ResponseCarat>().ReverseMap();
        CreateMap<Cut, ResponseCut>().ReverseMap();
        CreateMap<Color, ResponseColor>().ReverseMap();
        CreateMap<Clarity, ResponseClarity>().ReverseMap();
        //
        CreateMap<Polish, ResponsePolish>().ReverseMap();
        CreateMap<Symmetry, ResponseSymmetry>().ReverseMap();
        CreateMap<Fluorescence, ResponseFluorescence>().ReverseMap();
        CreateMap<Origin, ResponseOrigin>().ReverseMap();
        CreateMap<Shape, ResponseShape>().ReverseMap();

        //ProductDiamond
        CreateMap<ProductDiamond, RequestCreateProductDiamond>().ReverseMap();

        //ProductMaterial
        CreateMap<ProductMaterial, RequestCreateProductMaterial>().ReverseMap();
        CreateMap<ProductMaterial, RequestUpdateProductMaterial>().ReverseMap();

    }
}