using JSSATSProject.Repository.Entities;


namespace JSSATSProject.Service.Models.PromotionModel
{
    public class ResponsePromotion
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int DiscountRate { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Status { get; set; }

        public virtual ICollection<ProductCategory> Categories { get; set; } =  new List<ProductCategory>();
    }
}
