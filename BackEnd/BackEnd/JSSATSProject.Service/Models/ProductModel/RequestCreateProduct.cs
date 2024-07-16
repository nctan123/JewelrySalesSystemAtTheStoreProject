using Microsoft.AspNetCore.Http;
﻿using System.ComponentModel.DataAnnotations;


namespace JSSATSProject.Service.Models.ProductModel;

public class RequestCreateProduct
{
    public int CategoryId { get; set; }
    
    public string Name { get; set; } = null!;
    
    [Range(0, 99999999.99)]
    public decimal? MaterialCost { get; set; }
    
    [Range(0, 99999999.99)]
    public decimal? ProductionCost { get; set; }
    
    [Range(0, 99999999.99)]
    public decimal? GemCost { get; set; }

    [Range(0, 5)]
    public decimal PriceRate { get; set; }

    public IFormFile ImgFile { get; set; }

}