﻿namespace JSSATSProject.Service.Models.PaymentModel;

public class RequestCreateVnPayment
{
    public int PaymentId { get; set; }
    public int OrderId { get; set; }

    public int PaymentMethodId { get; set; }
    public int CustomerId { get; set; }

    public DateTime CreateDate { get; set; }
    public decimal Amount { get; set; }
}