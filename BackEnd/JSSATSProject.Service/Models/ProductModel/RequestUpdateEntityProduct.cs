using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Models.ProductModel
{
    public class RequestUpdateEntityProduct
    {
        public int CategoryId { get; set; }
        public int? StallsId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? MaterialCost { get; set; }
        public decimal? ProductionCost { get; set; }
        public decimal? GemCost { get; set; }
        public string Img { get; set; }
        public decimal PriceRate { get; set; }

        // ProductMaterial
        public int? MaterialId { get; set; }
        public decimal? Weight { get; set; }

        //ProductDiamond 
        public int? DiamondId { get; set; }

        public string? DiamondCode { get; set; }

        public string? DiamondName { get; set; }

        public int? OriginId { get; set; }

        public int? ShapeId { get; set; }

        public int? FluorescenceId { get; set; }

        public int? ColorId { get; set; }

        public int? SymmetryId { get; set; }

        public int? PolishId { get; set; }

        public int? CutId { get; set; }

        public int? ClarityId { get; set; }

        public int? CaratId { get; set; }

        public string? Status { get; set; }

        public string? DiamondGradingCode { get; set; }

    }
}
