﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace JSSATSProject.Repository.Entities;

public partial class PaymentDetail
{
    public int Id { get; set; }

    public int PaymentId { get; set; }

    public int PaymentMethodId { get; set; }

    public decimal Amount { get; set; }

    public string ExternalTransactionCode { get; set; }

    public string Status { get; set; }

    public virtual Payment Payment { get; set; }

    public virtual PaymentMethod PaymentMethod { get; set; }
}