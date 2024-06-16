﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace JSSATSProject.Repository.Entities;

public partial class BuyOrderDetail
{
    public int Id { get; set; }

    public int BuyOrderId { get; set; }

    public int CategoryTypeId { get; set; }

    public string DiamondGradingCode { get; set; }

    public int? MaterialId { get; set; }

    public decimal? MaterialWeight { get; set; }

    public int? PurchasePriceRatioId { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual BuyOrder BuyOrder { get; set; }

    public virtual ProductCategoryType CategoryType { get; set; }

    public virtual Material Material { get; set; }

    public virtual PurchasePriceRatio PurchasePriceRatio { get; set; }
}