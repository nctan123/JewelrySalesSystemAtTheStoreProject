﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.ProductMaterialModel
{
    public class RequestCreateProductMaterial
    {
        public int MaterialId { get; set; }

        public int ProductId { get; set; }

        public decimal? Weight { get; set; }
    }
}
