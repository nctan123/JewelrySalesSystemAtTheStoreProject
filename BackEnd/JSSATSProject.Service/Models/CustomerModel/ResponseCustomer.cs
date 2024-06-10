
using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Models.CustomerModel
{
    public class ResponseCustomer
    {
        public int Id { get; set; }
        public int? PointId { get; set; }
        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Gender { get; set; }

        public string? Address { get; set; }
        public int AvaliablePoint { get; set; }
        public int TotalPoint { get; set; }
        public virtual ICollection<Order>? Orders { get; set; } 
        public virtual ICollection<Payment>? Payments { get; set; }
    
    }
}
