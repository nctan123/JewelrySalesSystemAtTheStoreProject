using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.DiamondModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

public class DiamondService : IDiamondService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public DiamondService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }



    public async Task<ResponseModel> CreateDiamondAsync(RequestCreateDiamond requestDiamond)
    {
        // Create DiamondName
        var originName = await _unitOfWork.Context.Origins
            .Where(o => o.Id == requestDiamond.OriginId)
            .Select(o => o.Name)
            .FirstOrDefaultAsync();

        var shapeName = await _unitOfWork.Context.Shapes
            .Where(s => s.Id == requestDiamond.ShapeId)
            .Select(s => s.Name)
            .FirstOrDefaultAsync();

        var clarityLevel = await _unitOfWork.Context.Clarities
            .Where(c => c.Id == requestDiamond.ClarityId)
            .Select(c => c.Level)
            .FirstOrDefaultAsync();

        var newName = $"{originName}-{shapeName}-{clarityLevel}";
        // Create Code
        var newCode =  await GenerateUniqueCodeAsync();

        //Map
        var entity = _mapper.Map<Diamond>(requestDiamond);

        entity.Name = newName;
        entity.Code = newCode;

        await _unitOfWork.DiamondRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();

        return new ResponseModel
        {
            Data = entity,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetAllAsync()
    {
        var entities =
            await _unitOfWork.DiamondRepository.GetAsync(
                includeProperties: "Carat,Clarity,Color,Cut,Fluorescence,Origin,Polish,Shape,Symmetry");

        var response = entities.Select(diamond => new ResponseDiamond
        {
            Id = diamond.Id,
            Code = diamond.Code,
            Name = diamond.Name,
            OriginName = diamond.Origin.Name,
            ShapeName = diamond.Shape.Name,
            FluorescenceName = diamond.Fluorescence.Level,
            ColorName = diamond.Color.Name,
            SymmetryName = diamond.Symmetry.Level,
            PolishName = diamond.Polish.Level,
            CutName = diamond.Cut.Level,
            ClarityName = diamond.Clarity.Level,
            CaratWeight = diamond.Carat.Weight,
            DiamondGradingCode = diamond.DiamondGradingCode,
        }).ToList();

        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetByCodeAsync(string code)
    {
        var entities = await _unitOfWork.DiamondRepository.GetAsync(
            c => c.Code.Equals(code),
            includeProperties: "Carat,Clarity,Color,Cut,Fluorescence,Origin,Polish,Shape,Symmetry");

        var response = entities.Select(diamond => new ResponseDiamond
        {
            Id = diamond.Id,
            Code = diamond.Code,
            Name = diamond.Name,
            OriginName = diamond.Origin.Name,
            ShapeName = diamond.Shape.Name,
            FluorescenceName = diamond.Fluorescence.Level,
            ColorName = diamond.Color.Name,
            SymmetryName = diamond.Symmetry.Level,
            PolishName = diamond.Polish.Level,
            CutName = diamond.Cut.Level,
            ClarityName = diamond.Clarity.Level,
            CaratWeight = diamond.Carat.Weight,
            DiamondGradingCode = diamond.DiamondGradingCode,
        }).ToList();

        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetByIdAsync(int id)
    {
        var entities = await _unitOfWork.DiamondRepository.GetAsync(
            c => c.Id.Equals(id),
            includeProperties: "Carat,Clarity,Color,Cut,Fluorescence,Origin,Polish,Shape,Symmetry");

        var response = entities.Select(diamond => new ResponseDiamond
        {
            Id = diamond.Id,
            Code = diamond.Code,
            Name = diamond.Name,
            OriginName = diamond.Origin.Name,
            ShapeName = diamond.Shape.Name,
            FluorescenceName = diamond.Fluorescence.Level,
            ColorName = diamond.Color.Name,
            SymmetryName = diamond.Symmetry.Level,
            PolishName = diamond.Polish.Level,
            CutName = diamond.Cut.Level,
            ClarityName = diamond.Clarity.Level,
            CaratWeight = diamond.Carat.Weight,
            DiamondGradingCode = diamond.DiamondGradingCode,
        }).ToList();

        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetByNameAsync(string name)
    {
        var entities = await _unitOfWork.DiamondRepository.GetAsync(
            c => c.Name.Equals(name),
            includeProperties: "Carat,Clarity,Color,Cut,Fluorescence,Origin,Polish,Shape,Symmetry");

        var response = entities.Select(diamond => new ResponseDiamond
        {
            Id = diamond.Id,
            Code = diamond.Code,
            Name = diamond.Name,
            OriginName = diamond.Origin.Name,
            ShapeName = diamond.Shape.Name,
            FluorescenceName = diamond.Fluorescence.Level,
            ColorName = diamond.Color.Name,
            SymmetryName = diamond.Symmetry.Level,
            PolishName = diamond.Polish.Level,
            CutName = diamond.Cut.Level,
            ClarityName = diamond.Clarity.Level,
            CaratWeight = diamond.Carat.Weight,
            DiamondGradingCode = diamond.DiamondGradingCode,
        }).ToList();

        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> UpdateDiamondAsync(int diamondId, RequestUpdateDiamond requestDiamond)
    {
        try
        {
            var diamond = await _unitOfWork.DiamondRepository.GetByIDAsync(diamondId);
            if (diamond != null)
            {
                _mapper.Map(requestDiamond, diamond);

                await _unitOfWork.DiamondRepository.UpdateAsync(diamond);

                return new ResponseModel
                {
                    Data = diamond,
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
       
            return new ResponseModel
            {
                Data = null,
                MessageError = "An error occurred while updating the customer: " + ex.Message
            };
        }
    }

    public async Task<ResponseModel> UpdateStatusDiamondAsync(int diamondId, RequestUpdateStatusDiamond requestDiamond)
    {
        try
        {
            var diamond = await _unitOfWork.DiamondRepository.GetByIDAsync(diamondId);
            if (diamond != null)
            {
                _mapper.Map(requestDiamond, diamond);

                await _unitOfWork.DiamondRepository.UpdateAsync(diamond);

                return new ResponseModel
                {
                    Data = diamond,
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
           
            return new ResponseModel
            {
                Data = null,
                MessageError = "An error occurred while updating the customer: " + ex.Message
            };
        }
    }

    public async Task<string> GenerateUniqueCodeAsync()
    {
        string newCode;
        do
        {
            var prefix = DiamondConstants.DiamondPrefix;
            newCode = prefix + CustomLibrary.RandomNumber(3);
        }
        while (await _unitOfWork.Context.Diamonds.AnyAsync(so => so.Code == newCode));
        return newCode;
    }
}