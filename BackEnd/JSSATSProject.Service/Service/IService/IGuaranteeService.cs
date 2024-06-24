﻿using JSSATSProject.Service.Models.DiamondModel;
using JSSATSProject.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Models.ProductModel;

namespace JSSATSProject.Service.Service.IService
{
    public interface IGuaranteeService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> GetByProductIdAsync(int productId);
        public Task<ResponseModel> CreateGuaranteeAsync(List<ResponseProductDetails> products);
        public Task<ResponseModel> UpdateGuaranteeAsync(int guaranteeId, RequestUpdateGuarantee requestGuarantee);
        public Task<Guarantee?> GetEntityByCodeAsync(string guaranteeCode);
        public Task<ResponseProductForCheckOrder> GetResponseProductForCheckOrder(Guarantee guarantee);
    }
}