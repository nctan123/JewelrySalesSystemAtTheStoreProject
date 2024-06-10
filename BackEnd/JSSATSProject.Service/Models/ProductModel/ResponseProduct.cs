﻿using JSSATSProject.Repository.Entities;
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

        public string CategoryName { get; set; }
      
        public int CategoryId { get; set; }

        public string? Code { get; set; }

        public string Name { get; set; } = null!;

        public string? Img { get; set; }

        public decimal ProductValue { get; set; }

        public decimal PriceRate { get; set; }

        public string? Status { get; set; }
        public Diamond? Diamond { get; set; }

        [JsonIgnore]
        public ICollection<ProductDiamond>? ProductDiamonds { get; set; }
        [JsonIgnore]
        public ICollection<ProductMaterial>? ProductMaterials { get; set; }
    }
}
