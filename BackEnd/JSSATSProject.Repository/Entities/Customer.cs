﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JSSATSProject.Repository.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public int? PointId { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Gender { get; set; }

    public string Address { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual ICollection<BuyOrder> BuyOrders { get; set; } = new List<BuyOrder>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Point Point { get; set; }
    [JsonIgnore]
    public virtual ICollection<SellOrder> SellOrders { get; set; } = new List<SellOrder>();
    [JsonIgnore]
    public virtual ICollection<SpecialDiscountRequest> SpecialDiscountRequests { get; set; } = new List<SpecialDiscountRequest>();
}