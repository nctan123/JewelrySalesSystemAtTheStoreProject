
﻿using System.ComponentModel.DataAnnotations;
using JSSATSProject.Repository.CustomValidators;
using JSSATSProject.Repository.Enums;


namespace JSSATSProject.Service.Models.OrderModel;

public class RequestCreateSellOrder
{
    public int? Id { get; set; }
    [VietnamesePhone(ErrorMessage = "{0} must be a valid Vietnamese phone number.")]
    public required string CustomerPhoneNumber { get; set; }
    public required int StaffId { get; set; }
    public DateTime CreateDate { get; set; }
    [Range(0, Int32.MaxValue)]
    public int DiscountPoint { get; set; } = 0;
    public string? Description { get; set; }
    public required Dictionary<string, int> ProductCodesAndQuantity { get; set; }
    public required Dictionary<string, int?>? ProductCodesAndPromotionIds { get; set; }
    public required int? SpecialDiscountRequestId { get; set; }

    //for handing special discount
    public bool IsSpecialDiscountRequested { get; set; }
    //[Range(0d,1d)]
    public decimal? SpecialDiscountRate { get; set; }

    public string? DiscountRejectedReason { get; set; }
    public SpecialDiscountRequestStatus? SpecialDiscountRequestStatus { get; set; }
}