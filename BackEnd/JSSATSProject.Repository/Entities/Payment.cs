﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace JSSATSProject.Repository.Entities;

public partial class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreateDate { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual Order Order { get; set; }

    public virtual ICollection<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();
}