﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JSSATSProject.Repository.Entities;

public partial class Shape
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
    [JsonIgnore]
    public decimal? PriceRate { get; set; }
    [JsonIgnore]
    public virtual ICollection<Diamond> Diamonds { get; set; } = new List<Diamond>();
}