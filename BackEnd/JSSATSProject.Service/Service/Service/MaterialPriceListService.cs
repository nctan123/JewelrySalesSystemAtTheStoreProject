using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Repository.Repos;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.MaterialPriceListModel;
using JSSATSProject.Service.Service.IService;
using System.Linq.Expressions;

namespace JSSATSProject.Service.Service.Service;

public class MaterialPriceListService : IMaterialPriceListService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public MaterialPriceListService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel> CreateMaterialPriceListAsync(
        RequestCreateMaterialPriceList requestMaterialPriceList)
    {
        var entity = _mapper.Map<MaterialPriceList>(requestMaterialPriceList);
        if (requestMaterialPriceList.BuyPrice > requestMaterialPriceList.SellPrice)
        {
            return new ResponseModel
            {
                MessageError = "Material buy price cannot larger than sell price"
            };
        }
        await _unitOfWork.MaterialPriceListRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();
        return new ResponseModel
        {
            Data = entity,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetAllAsync(DateTime? effectiveDate, int page = 1, int pageSize = 10)
    {
        try
        {
            if (effectiveDate.HasValue)
            {
                var targetDateTime = effectiveDate.Value;

                //  EffectiveDate <= targetDateTime
                var maxEffectiveDate = await _unitOfWork.MaterialPriceListRepository
                    .GetAsync(
                        filter: x => x.EffectiveDate <= targetDateTime,
                        orderBy: x => x.OrderByDescending(e => e.EffectiveDate),
                        pageIndex: 1,
                        pageSize: 1
                    )
                    .ContinueWith(task => task.Result.FirstOrDefault()?.EffectiveDate);

                if (maxEffectiveDate == null)
                {
                    
                    return new ResponseModel
                    {
                        TotalPages = 0,
                        TotalElements = 0,
                        Data = new List<ResponseMaterialPriceList>(),
                        MessageError = "No records found.",
                    };
                }

                
                Expression<Func<MaterialPriceList, bool>> filter = x => x.EffectiveDate == maxEffectiveDate;
                var entities = await _unitOfWork.MaterialPriceListRepository.GetAsync(
                    filter: filter,
                    orderBy: x => x.OrderBy(e => e.EffectiveDate),
                    pageIndex: page,
                    pageSize: pageSize
                );

                var totalCount = await _unitOfWork.MaterialPriceListRepository.CountAsync(filter);
                var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                
                var response = _mapper.Map<List<ResponseMaterialPriceList>>(entities);

                return new ResponseModel
                {
                    TotalPages = totalPages,
                    TotalElements = totalCount,
                    Data = response,
                    MessageError = "",
                };
            }
            else
            {
                var entities = await _unitOfWork.MaterialPriceListRepository.GetAsync(
                    orderBy: x => x.OrderBy(e => e.EffectiveDate),
                    pageIndex: page,
                    pageSize: pageSize
                );

                var totalCount = await _unitOfWork.MaterialPriceListRepository.CountAsync(null);
                var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                var response = _mapper.Map<List<ResponseMaterialPriceList>>(entities);

                return new ResponseModel
                {
                    TotalPages = totalPages,
                    TotalElements = totalCount,
                    Data = response,
                    MessageError = "",
                };
            }
        }
        catch (Exception ex)
        {
            return new ResponseModel
            {
                TotalPages = 0,
                TotalElements = 0,
                Data = new List<ResponseMaterialPriceList>(),
                MessageError = $"An error occurred: {ex.Message}",
            };
        }
    }






    public async Task<ResponseModel> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.MaterialPriceListRepository.GetByIDAsync(id);
        var response = _mapper.Map<ResponseMaterialPriceList>(entity);
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> UpdateMaterialPriceListAsync(int materialpricelistId,
        RequestUpdateMaterialPriceList requestMaterialPriceList)
    {
        try
        {
            var materialpricelist = await _unitOfWork.MaterialPriceListRepository.GetByIDAsync(materialpricelistId);
            if (materialpricelist != null)
            {
                _mapper.Map(requestMaterialPriceList, materialpricelist);

                await _unitOfWork.MaterialPriceListRepository.UpdateAsync(materialpricelist);

                return new ResponseModel
                {
                    Data = materialpricelist,
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
}