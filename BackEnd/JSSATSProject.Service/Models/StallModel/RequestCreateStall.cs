﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.StallModel
{
    public class RequestCreateStall
    {
        public string Name { get; set; } = null!;

        public int TypeId { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }
    }
}
