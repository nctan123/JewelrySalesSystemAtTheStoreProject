using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.ProductModel
{
    public class ResponseProduct
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int? StallsId { get; set; }

        public string? Code { get; set; }

        public string Name { get; set; } = null!;

        public decimal? MaterialCost { get; set; }

        public decimal? ProductionCost { get; set; }

        public decimal? GemCost { get; set; }

        public string? Img { get; set; }

        public decimal PriceRate { get; set; }

        public string? Status { get; set; }
    }
}
