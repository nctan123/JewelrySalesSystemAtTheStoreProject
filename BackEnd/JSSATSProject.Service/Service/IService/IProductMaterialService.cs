using JSSATSProject.Service.Models.ProductDiamondModel;
using JSSATSProject.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSSATSProject.Service.Models.ProductMaterialModel;

namespace JSSATSProject.Service.Service.IService
{
    public interface IProductMaterialService
    {
        Task<ResponseModel> CreateAsync(RequestCreateProductMaterial requestProductMaterial);
        Task<ResponseModel> UpdateAsync(RequestUpdateProductMaterial requestProductMaterial);
    }
}
