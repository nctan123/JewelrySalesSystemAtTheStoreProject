<<<<<<< HEAD
﻿using JSSATSProject.Repository.Enums;
=======
﻿using System.ComponentModel.DataAnnotations;
using JSSATSProject.Repository.Enums;
>>>>>>> BE_NhatAnh
﻿using System.Text.Json.Serialization;
using JSSATSProject.Repository.Enums;

namespace JSSATSProject.Service.Models.OrderModel;

public class RequestCreateSellOrder
{
    public int? Id { get; set; }
    [Phone]
    public required string CustomerPhoneNumber { get; set; }
    public required int StaffId { get; set; }
    public DateTime CreateDate { get; set; }
    [Range(0, (double)Int32.MaxValue)]
    public int DiscountPoint { get; set; }
    public string? Description { get; set; }
    public required Dictionary<string, int> ProductCodesAndQuantity { get; set; }
    public required Dictionary<string, int?>? ProductCodesAndPromotionIds { get; set; }
    public required int? SpecialDiscountRequestId { get; set; }

    //for handing special discount
    public bool IsSpecialDiscountRequested { get; set; }
    [Range(0d,1d)]
    public decimal? SpecialDiscountRate { get; set; }

    public string? DiscountRejectedReason { get; set; }
    public SpecialDiscountRequestStatus? SpecialDiscountRequestStatus { get; set; }
}