﻿using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Models.ProductModel;

public class ResponseProduct
{
    public int Id { get; set; }
    public string? CategoryName { get; set; }
    public int CategoryId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Img { get; set; }
    public decimal ProductValue { get; set; }
    public decimal PriceRate { get; set; }
    public string? Status { get; set; }
    public string? DiamondCode { get; set; }
    public string? DiamondName { get; set; }
    public string? MaterialName { get; set; }
    public decimal? MaterialWeight { get; set; }
    public int PromotionId { get; set; }
    public decimal? DiscountRate { get; set; }
    public virtual Stall? Stalls { get; set; }
}