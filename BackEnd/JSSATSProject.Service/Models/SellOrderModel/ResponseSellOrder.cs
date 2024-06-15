using JSSATSProject.Repository.Entities;



namespace JSSATSProject.Service.Models.OrderModel
{
    public class ResponseSellOrder
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int StaffId { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDraft { get; set; }

        public string? Type { get; set; }

        public string? Description { get; set; }

       // public virtual ICollection<OrderDetail> OrderDetails { get; set; } 

        
    }
}
