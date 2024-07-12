using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Service.Service.Service;

public class GuaranteeService : IGuaranteeService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public GuaranteeService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<ResponseModel> GetAllAsync()
    {
        var entities = await _unitOfWork.GuaranteeRepository.GetAsync();
        var response = _mapper.Map<List<ResponseGuarantee>>(entities);
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.GuaranteeRepository.GetByIDAsync(id);
        var response = _mapper.Map<ResponseGuarantee>(entity);
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetByProductIdAsync(int productId)
    {
        var entity = await _unitOfWork.GuaranteeRepository.GetByProductIdAsync(productId);
        var response = _mapper.Map<ResponseGuarantee>(entity);
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> UpdateGuaranteeAsync(int guaranteeId, RequestUpdateGuarantee requestGuarantee)
    {
        try
        {
            var guarantee = await _unitOfWork.GuaranteeRepository.GetByIDAsync(guaranteeId);
            if (guarantee != null)
            {
                _mapper.Map(requestGuarantee, guarantee);

                await _unitOfWork.GuaranteeRepository.UpdateAsync(guarantee);

                return new ResponseModel
                {
                    Data = guarantee,
                    MessageError = ""
                };
            }

            return new ResponseModel
            {
                Data = null,
                MessageError = "Not Found"
            };
        }
        catch (Exception ex)
        {
            // Log the exception and return an appropriate error response
            return new ResponseModel
            {
                Data = null,
                MessageError = "An error occurred while updating the customer: " + ex.Message
            };
        }
    }

    public async Task<Guarantee?> GetEntityByCodeAsync(string guaranteeCode)
    {
        var entity = await _unitOfWork.GuaranteeRepository.GetEntityByCodeAsync(guaranteeCode);
        return entity;
    }

    public async Task<ResponseProductForCheckOrder> GetResponseProductForCheckOrder(Guarantee guarantee)
    {
        var rate = await _unitOfWork.PurchasePriceRatioRepository.GetRate(guarantee.Product.Category.TypeId);
        return new ResponseProductForCheckOrder
        {
            Code = guarantee.Product.Code,
            Name = guarantee.Product.Name,
            Quantity = guarantee.SellOrderDetail.Quantity,
            PriceInOrder = guarantee.SellOrderDetail.UnitPrice,
            EstimateBuyPrice = rate * guarantee.SellOrderDetail.UnitPrice,
            ReasonForEstimateBuyPrice = $"The percentage of buyback value is {rate}"
        };
    }

    public async Task<ResponseModel> CreateGuaranteeAsync(List<ResponseProductDetails> products)
    {
        foreach (var product in products)
        {
            var guaranteeMonths = GetGuaranteeMonths(product.CategoryId);
            var newCode = await GenerateUniqueCodeAsync();

            if (guaranteeMonths > 0)
            {
                var guarantee = new Guarantee
                {
                    ProductId = product.Id,
                    Description = "Use the free service provided during the warranty period",
                    EffectiveDate = DateTime.UtcNow.Date,
                    ExpiredDate = DateTime.UtcNow.Date.AddMonths(guaranteeMonths),
                    SellorderdetailId = product.SellOrderDetailId,
                    Code = newCode
                };

                await _unitOfWork.GuaranteeRepository.InsertAsync(guarantee);
            }
        }

        await _unitOfWork.SaveAsync();

        return new ResponseModel
        {
            Data = "",
            MessageError = ""
        };
    }

    private int GetGuaranteeMonths(int categoryId)
    {
        switch (categoryId)
        {
            case ProductConstants.RingCategory:
                return GuaranteeConstants.RING_MONTHS;
            case ProductConstants.EarringsCategory:
                return GuaranteeConstants.EARRINGS_MONTHS;
            case ProductConstants.BraceletCategory:
                return GuaranteeConstants.BRACELET_MONTHS;
            case ProductConstants.NecklaceCategory:
                return GuaranteeConstants.NECKLACE_MONTHS;
            case ProductConstants.DiamondsCategory:
                return GuaranteeConstants.DIAMONDS_MONTHS;
            default:
                return 0;
        }
    }

    public async Task<string> GenerateUniqueCodeAsync()
    {
        string newCode;
        do
        {
            var prefix = GuaranteeConstants.GuaranteePrefix;
            newCode = prefix + CustomLibrary.RandomString(14 - prefix.Length);
        }
        while (await _unitOfWork.Context.Guarantees.AnyAsync(so => so.Code == newCode));
        return newCode;
    }


}