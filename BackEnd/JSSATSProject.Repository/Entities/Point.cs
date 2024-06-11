﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace JSSATSProject.Repository.Entities;

public partial class Point
{
    public int Id { get; set; }

    public int? AvailablePoint { get; set; }

    public int? Totalpoint { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public Point()
    {
        
    }

    public Point(int id, int? availablePoint, int? totalPoint, ICollection<Customer> customers)
    {
        Id = id;
        AvailablePoint = availablePoint;
        Totalpoint = totalPoint;
        Customers = customers;
    }

}