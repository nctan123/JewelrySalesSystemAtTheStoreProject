﻿using JSSATSProject.Repository.Entities;



namespace JSSATSProject.Service.Models.OrderModel
{
    public class ResponseSellOrder
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int StaffId { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public int DiscountPoint { get; set; } = 0;

        public int? SpecialDiscountRequestId { get; set; }

        public virtual Customer Customer { get; set; }

        public string Code { get; set; }

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public virtual ICollection<SellOrderDetail> SellOrderDetails { get; set; } = new List<SellOrderDetail>();

        public virtual SpecialDiscountRequest SpecialDiscountRequest { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
