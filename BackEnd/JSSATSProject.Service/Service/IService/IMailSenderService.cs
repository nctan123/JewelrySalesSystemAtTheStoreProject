using JSSATSProject.Service.Models.MailModel;

namespace JSSATSProject.Service.Service.IService;

public interface IMailSenderService
{
    public Task SendEmailAsync(RequestSendMail requestSendMail);

}