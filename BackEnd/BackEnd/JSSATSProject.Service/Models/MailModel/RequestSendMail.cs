namespace JSSATSProject.Service.Models.MailModel;

public class RequestSendMail
{
    public required string ToAddress { get; set; }
    public required string Subject { get; set; }
    public required int SellOrderId { get; set; }
    public required decimal TotalPrice { get; set; }
    public required decimal PromotionDiscount { get; set; }
    public required decimal PointDiscount { get; set; }
    
}