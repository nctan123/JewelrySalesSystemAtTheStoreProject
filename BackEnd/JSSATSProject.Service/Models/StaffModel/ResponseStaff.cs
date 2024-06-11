
using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Models.StaffModel
{
    public class ResponseStaff
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Address { get; set; }

        public string? Gender { get; set; }

        public string? Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public decimal TotalRevennue { get; set; }

        public int TotalOrder { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
