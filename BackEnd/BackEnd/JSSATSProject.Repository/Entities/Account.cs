﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JSSATSProject.Repository.Entities;

public partial class Account
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Status { get; set; }

    public virtual Role Role { get; set; }
    [JsonIgnore]
    public virtual Staff Staff { get; set; }
}