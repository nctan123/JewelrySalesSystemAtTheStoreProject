using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models.SellOrderDetailsModel;


namespace JSSATSProject.Service.Models.OrderModel
{
    public class ResponseSellOrder
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int DiscountPoint { get; set; } = 0;
        public decimal TotalAmount { get; set; }
        public int? SpecialDiscountRequestId { get; set; }
        public virtual ICollection<ResponseSellOrderDetails> SellOrderDetails { get; set; } = new List<ResponseSellOrderDetails>();
        
        public virtual SpecialDiscountRequest SpecialDiscountRequest { get; set; }
    }
}