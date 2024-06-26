﻿namespace JSSATSProject.Service.Models.StaffModel;

public class ResponseStaff
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public string? Status { get; set; }

    public decimal TotalRevenue { get; set; }
    public int TotalSellOrder { get; set; }
}