﻿using JSSATSProject.Service.Models.PaymentModel;
using Microsoft.AspNetCore.Http;

namespace JSSATSProject.Service.Service.IService;

public interface IVnPayService
{
    string CreatePaymentUrl(RequestCreateVnPayment model, HttpContext context);
    ResponseVnPayment PaymentExecute(IQueryCollection collections);
}