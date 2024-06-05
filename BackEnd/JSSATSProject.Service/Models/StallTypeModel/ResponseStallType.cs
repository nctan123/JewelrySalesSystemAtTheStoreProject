using JSSATSProject.Repository.Entities;



namespace JSSATSProject.Service.Models.StallTypeModel
{
    public class ResponseStallType
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Status { get; set; }

        public virtual ICollection<Stall> Stalls { get; set; } = new List<Stall>();
    }
}
