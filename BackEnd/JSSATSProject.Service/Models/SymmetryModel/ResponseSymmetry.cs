using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.SymmetryModel
{
    public class ResponseSymmetry
    {
        public int Id { get; set; }

        public string Level { get; set; }

        public string Description { get; set; }

        public decimal? PriceRate { get; set; }
    }
}
