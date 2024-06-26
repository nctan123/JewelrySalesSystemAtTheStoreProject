﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace JSSATSProject.Repository.Entities;

public partial class PromotionRequest
{
    public int RequestId { get; set; }

    public int ManagerId { get; set; }

    public string Description { get; set; }

    public decimal DiscountRate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? ApprovedBy { get; set; }
    
    public virtual Staff ApprovedByNavigation { get; set; }

    public virtual Staff Manager { get; set; }

    public virtual ICollection<ProductCategory> Categories { get; set; } = new List<ProductCategory>();
}