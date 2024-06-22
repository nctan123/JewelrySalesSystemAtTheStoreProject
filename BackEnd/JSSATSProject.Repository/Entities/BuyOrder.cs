﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;

namespace JSSATSProject.Repository.Entities;

public partial class BuyOrder
{
    public int Id { get; set; }
    public string Code { get; set; }
    [JsonIgnore]

    public int CustomerId { get; set; }
    [JsonIgnore]

    public int StaffId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreateDate { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public virtual ICollection<BuyOrderDetail> BuyOrderDetails { get; set; } = new List<BuyOrderDetail>();
    
    [JsonIgnore]
    public virtual Customer Customer { get; set; }
    
    [JsonIgnore]
    public virtual Staff Staff { get; set; }

    public BuyOrder()
    {
        //14 chars in total
        var prefix = OrderConstants.BuyOrderCodePrefix;
        Code = Code ?? prefix + CustomLibrary.RandomString(14 - prefix.Length);
    }
}