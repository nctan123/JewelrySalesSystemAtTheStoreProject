﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace JSSATSProject.Repository.Entities;

public partial class Material
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<BuyOrderDetail> BuyOrderDetails { get; set; } = new List<BuyOrderDetail>();

    public virtual ICollection<MaterialPriceList> MaterialPriceLists { get; set; } = new List<MaterialPriceList>();

    public virtual ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
}