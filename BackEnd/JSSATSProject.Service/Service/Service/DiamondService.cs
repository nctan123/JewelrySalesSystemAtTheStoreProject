﻿using JSSATSProject.Repository.Entities;
using JSSATSProject.Repository;
using JSSATSProject.Service.Models.DiamondModel;
using JSSATSProject.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JSSATSProject.Service.Service.IService;

public class DiamondService : IDiamondService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DiamondService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel> CreateDiamondAsync(RequestCreateDiamond requestDiamond)
    {
        var entity = _mapper.Map<Diamond>(requestDiamond);
        await _unitOfWork.DiamondRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();
        return new ResponseModel
        {
            Data = entity,
            MessageError = "",
        };
    }

    public async Task<ResponseModel> GetAllAsync()
    {
        var entities = await _unitOfWork.DiamondRepository.GetAsync();
        var response = _mapper.Map<List<ResponseDiamond>>(entities);
        return new ResponseModel
        {
            Data = response,
            MessageError = "",
        };
    }

    public async Task<ResponseModel> GetByCodeAsync(string code)
    {
        var entities = await _unitOfWork.DiamondRepository.GetAsync(c => c.Code.Equals(code), null, "");
        if (!entities.Any())
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = $"Diamond with code '{code}' not found.",
            };
        }

        var response = _mapper.Map<List<ResponseDiamond>>(entities);
        return new ResponseModel
        {
            Data = response,
            MessageError = "",
        };
    }

    public async Task<ResponseModel> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.DiamondRepository.GetByIDAsync(id);
        var response = _mapper.Map<ResponseDiamond>(entity);
        return new ResponseModel
        {
            Data = response,
            MessageError = "",
        };
    }

    public async Task<ResponseModel> GetByNameAsync(string name)
    {
        var entities = await _unitOfWork.DiamondRepository.GetAsync(c => c.Name.Equals(name), null, "");
        if (!entities.Any())
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = $"Diamond with name '{name}' not found.",
            };
        }

        var response = _mapper.Map<List<ResponseDiamond>>(entities);
        return new ResponseModel
        {
            Data = response,
            MessageError = "",
        };
    }

    public async Task<ResponseModel> UpdateDiamondAsync(int diamondId, RequestUpdateDiamond requestDiamond)
    {
        var diamond = await _unitOfWork.DiamondRepository.GetByIDAsync(diamondId);
        if (diamond == null)
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = "Diamond not found.",
            };
        }

        _mapper.Map(requestDiamond, diamond);
        await _unitOfWork.DiamondRepository.UpdateAsync(diamond);
        await _unitOfWork.SaveAsync();

        return new ResponseModel
        {
            Data = diamond,
            MessageError = "",
        };
    }
}