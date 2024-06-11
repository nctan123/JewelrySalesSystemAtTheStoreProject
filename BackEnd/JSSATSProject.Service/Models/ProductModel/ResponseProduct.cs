using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.ProductModel
{
    public class ResponseProduct
    {
        public int Id { get; set; }
<<<<<<< HEAD
        public string? Category { get; set; }
        // [JsonIgnore]
=======
>>>>>>> ef1d898c610203bb40990ce34f1644abc601b704
        public int CategoryId { get; set; }

        public string? Code { get; set; }

        public string Name { get; set; } = null!;

        public string? Img { get; set; }
<<<<<<< HEAD
        public decimal ProductValue { get; set; }
        
        [JsonIgnore]
        public decimal PriceRate { get; set; }

        public string? Status { get; set; }
        public string? DiamondCode { get; set; }
        public string? DiamondName { get; set; }
        public string? MaterialName { get; set; }
        public decimal? MaterialWeight { get; set; }
        
        [JsonIgnore]
        public ICollection<ProductDiamond>? ProductDiamonds { get; set; }
        [JsonIgnore]
        public ICollection<ProductMaterial>? ProductMaterials { get; set; }
        
=======

        public decimal ProductValue { get; set; }

        public decimal PriceRate { get; set; }

        public string? Status { get; set; }

        public ICollection<ProductDiamond>? ProductDiamonds { get; set; }

        public ICollection<ProductMaterial>? ProductMaterials { get; set; }
>>>>>>> ef1d898c610203bb40990ce34f1644abc601b704
    }
}