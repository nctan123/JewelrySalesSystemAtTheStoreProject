﻿using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JSSATSProject.Repository.Enums;


namespace JSSATSProject.Service.Models.OrderModel
{
    public class RequestCreateSellOrder
    {
        public required string CustomerPhoneNumber { get; set; }
        public required int StaffId { get; set; }
        public DateTime CreateDate { get; set; }

        public string? Description { get; set; }
        public required Dictionary<string, int> ProductCodesAndQuantity { get; set; }
        public required Dictionary<string, string> ProductCodesAndPromotionIds { get; set; }
        public required int? SpecialDiscountRequestId { get; set; }

        //for handing special discount
        public bool IsSpecialDiscountRequested { get; set; }
        public decimal? SpecialDiscountRate { get; set; }

        public string? DiscountRejectedReason { get; set; }

        public SpecialDiscountRequestStatus? SpecialDiscountRequestStatus { get; set; }
    }
}