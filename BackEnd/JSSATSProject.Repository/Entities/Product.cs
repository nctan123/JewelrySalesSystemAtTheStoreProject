﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JSSATSProject.Repository.Entities;

public partial class Product
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int? StallsId { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public decimal? MaterialCost { get; set; }

    public decimal? ProductionCost { get; set; }

    public decimal? GemCost { get; set; }

    public string Img { get; set; }

    public decimal PriceRate { get; set; }

    public string Status { get; set; }

    public virtual ProductCategory Category { get; set; }

    public virtual ICollection<Guarantee> Guarantees { get; set; } = new List<Guarantee>();
    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();

    public virtual Stall Stalls { get; set; }

    public virtual ICollection<ProductDiamond> ProductDiamonds { get; set; } = new List<ProductDiamond>();
}