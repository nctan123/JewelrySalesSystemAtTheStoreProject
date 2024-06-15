﻿using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Models;
using System.Threading.Tasks;
using JSSATSProject.Service.Models.PromotionModel;
using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Service.IService
{
    public interface IPromotionService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> CreatePromotionAsync(RequestCreatePromotion requestPromotion);
        public Task<ResponseModel> UpdatePromotionAsync(int promotionId, RequestUpdatePromotion requestPromotion);
       
    }
}