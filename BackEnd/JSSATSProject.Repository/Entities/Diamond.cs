﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JSSATSProject.Repository.Entities;

public partial class Diamond
{    [JsonIgnore]

    public int Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }
    [JsonIgnore]

    public int OriginId { get; set; }
    [JsonIgnore]

    public int ShapeId { get; set; }
    [JsonIgnore]

    public int FluorescenceId { get; set; }
    [JsonIgnore]

    public int ColorId { get; set; }
    [JsonIgnore]

    public int SymmetryId { get; set; }
    [JsonIgnore]

    public int PolishId { get; set; }
    [JsonIgnore]

    public int CutId { get; set; }
    [JsonIgnore]

    public int ClarityId { get; set; }
    [JsonIgnore]

    public int CaratId { get; set; }

    public string Status { get; set; }

    public virtual Carat Carat { get; set; }

    public virtual Clarity Clarity { get; set; }

    public virtual Color Color { get; set; }

    public virtual Cut Cut { get; set; }

    public virtual Fluorescence Fluorescence { get; set; }

    public virtual Origin Origin { get; set; }

    public virtual Polish Polish { get; set; }

    public virtual Shape Shape { get; set; }

    public virtual Symmetry Symmetry { get; set; }

    [JsonIgnore]
    public virtual ICollection<ProductDiamond> ProductDiamonds { get; set; } = new List<ProductDiamond>();

    public Diamond()
    {
    }

    public Diamond(int id, string code, string name, int originId, int shapeId, int fluorescenceId, int colorId, int symmetryId, int polishId, int cutId, int clarityId, int caratId, string status, Carat carat, Clarity clarity, Color color, Cut cut, Fluorescence fluorescence, Origin origin, Polish polish, Shape shape, Symmetry symmetry, ICollection<ProductDiamond> products)
    {
        Id = id;
        Code = code;
        Name = name;
        OriginId = originId;
        ShapeId = shapeId;
        FluorescenceId = fluorescenceId;
        ColorId = colorId;
        SymmetryId = symmetryId;
        PolishId = polishId;
        CutId = cutId;
        ClarityId = clarityId;
        CaratId = caratId;
        Status = status;
        Carat = carat;
        Clarity = clarity;
        Color = color;
        Cut = cut;
        Fluorescence = fluorescence;
        Origin = origin;
        Polish = polish;
        Shape = shape;
        Symmetry = symmetry;
<<<<<<< HEAD
        ProductDiamonds = products;
=======
>>>>>>> ef1d898c610203bb40990ce34f1644abc601b704
    }
}