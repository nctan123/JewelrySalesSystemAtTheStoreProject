using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductDiamondModel;

namespace JSSATSProject.Service.Service.IService
{
    public interface IProductDiamondService
    {
        Task<ResponseModel> CreateAsync(RequestCreateProductDiamond requestProductDiamond);
    }
}
