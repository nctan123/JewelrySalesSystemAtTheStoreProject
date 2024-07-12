﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;

namespace JSSATSProject.Repository.Entities;

public partial class SellOrder
{
    public int Id { get; set; }
    
    public string Code { get; set; }
    public int CustomerId { get; set; }

    public int StaffId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreateDate { get; set; }

    public string Status { get; set; }
    public string Description { get; set; }

    public int DiscountPoint { get; set; } = 0;

    public int? SpecialDiscountRequestId { get; set; }


    public virtual Customer Customer { get; set; }
    [JsonIgnore]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<SellOrderDetail> SellOrderDetails { get; set; } = new List<SellOrderDetail>();

    public virtual SpecialDiscountRequest SpecialDiscountRequest { get; set; }
    public virtual Staff Staff { get; set; }

    public SellOrder()
    {
    }
}