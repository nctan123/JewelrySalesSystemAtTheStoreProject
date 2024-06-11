﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace JSSATSProject.Repository.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int StaffId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDraft { get; set; }

    public string Type { get; set; }

    public string Description { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Staff Staff { get; set; }

    public Order()
    {
        
    }
    public Order(int id, int customerId, int staffId, decimal totalAmount, DateTime createDate, bool isDraft, string type, string description, Customer customer, ICollection<OrderDetail> orderDetails, ICollection<Payment> payments, Staff staff)
    {
        Id = id;
        CustomerId = customerId;
        StaffId = staffId;
        TotalAmount = totalAmount;
        CreateDate = createDate;
        IsDraft = isDraft;
        Type = type;
        Description = description;
        Customer = customer;
        OrderDetails = new List<OrderDetail>(orderDetails);
        Payments = new List<Payment>(payments);
        Staff = staff;
    }
}