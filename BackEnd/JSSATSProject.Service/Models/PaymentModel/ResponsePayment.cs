﻿namespace JSSATSProject.Service.Models.PaymentModel;

public class ResponsePayment
{
    public int Id { get; set; }

    public int? SellorderId { get; set; }

    public int? BuyorderId { get; set; }
    public string? SellOrderCode { get; set; }

    public string? BuyOrderCode { get; set; }

    public string CustomerPhone { get; set; }

    public string CustomerName { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreateDate { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; }
    public string ExternalTransactionCode { get; set; }

    public string PaymentMethodName { get; set; }
}