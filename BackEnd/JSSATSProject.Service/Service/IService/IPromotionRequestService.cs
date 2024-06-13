using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.PromotionRequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface IPromotionRequestService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> CreatePromotionRequestAsync(CreatePromotionRequest promotionRequest);
        public Task<ResponseModel> UpdatePromotionRequestAsync(int promotionrequestId, UpdatePromotionRequest promotionRequest);
    }
}
