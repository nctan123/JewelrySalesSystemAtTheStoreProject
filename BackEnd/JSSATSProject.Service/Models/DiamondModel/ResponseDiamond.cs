using JSSATSProject.Repository.Entities;



namespace JSSATSProject.Service.Models.DiamondModel
{
    public class ResponseDiamond
    {
        public int Id { get; set; }
<<<<<<< HEAD

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Origin { get; set; }

        public string Shape{ get; set; }

        public string Fluorescence { get; set; }

        public string Color{ get; set; }

        public string Symmetry{ get; set; }

        public string Polish { get; set; }

        public string Cut { get; set; }

        public string Clarity{ get; set; }

        public string Carat { get; set; }

        public string? Status { get; set; }
=======
        public string Code { get; set; }
        public string Name { get; set; }
        public string OriginName { get; set; }
        public string ShapeName { get; set; }
        public string FluorescenceName { get; set; }
        public string ColorName { get; set; }
        public string SymmetryName { get; set; }
        public string PolishName { get; set; }
        public string CutName { get; set; }
        public string ClarityName { get; set; }
        public decimal CaratWeight { get; set; }
        public string Status { get; set; }


>>>>>>> ef1d898c610203bb40990ce34f1644abc601b704
    }
}
