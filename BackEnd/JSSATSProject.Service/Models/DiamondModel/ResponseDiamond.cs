using JSSATSProject.Repository.Entities;



namespace JSSATSProject.Service.Models.DiamondModel
{
    public class ResponseDiamond
    {
        public int Id { get; set; }
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


    }
}
