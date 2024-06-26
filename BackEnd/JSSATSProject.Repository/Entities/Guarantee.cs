﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;
using System;
using System.Collections.Generic;

namespace JSSATSProject.Repository.Entities;

public partial class Guarantee
{
    public int Id { get; set; }
    
    public string Code { get; set; }
    
    public int ProductId { get; set; }

    public string Description { get; set; }

    public DateTime EffectiveDate { get; set; }

    public int? SellorderdetailId { get; set; }

    public DateTime ExpiredDate { get; set; }

    public virtual Product Product { get; set; }

    public virtual SellOrderDetail SellOrderDetail { get; set; }

    public Guarantee()
    {
        var prefix = GuaranteeContants.GuaranteePrefix;
        Code = Code ?? prefix + CustomLibrary.RandomString(14 - prefix.Length);
    }


    

}