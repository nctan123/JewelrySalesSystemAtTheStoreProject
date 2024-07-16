using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Repository.Repos
{
    public class ProductDiamondRespository : GenericRepository<ProductDiamond>
    {
        public ProductDiamondRespository(DBContext context) : base(context)
        {
        }
    }

}
