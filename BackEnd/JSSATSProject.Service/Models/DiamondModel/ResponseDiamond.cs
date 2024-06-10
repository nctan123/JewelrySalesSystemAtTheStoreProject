using JSSATSProject.Repository.Entities;



namespace JSSATSProject.Service.Models.DiamondModel
{
    public class ResponseDiamond
    {
        public int Id { get; set; }

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
    }
}
