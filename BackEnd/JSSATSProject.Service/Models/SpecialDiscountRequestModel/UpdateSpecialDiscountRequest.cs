using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSSATSProject.Repository.Enums;

namespace JSSATSProject.Service.Models.SpecialDiscountRequestModel
{
    public class UpdateSpecialDiscountRequest
    {
        public required string Status { get; set; }
        public int? ApprovedBy { get; set; }
    }
}