using JSSATSProject.Service.Models.MailModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailSenderService _mailSenderService;

        public MailController(IMailSenderService mailSenderService)
        {
            _mailSenderService = mailSenderService;
        }


        [HttpPost]
        [Route("Send")]
        public async Task<IActionResult> SendAsync([FromBody] RequestSendMail requestSendMail)
        {
            await _mailSenderService.SendEmailAsync(requestSendMail);
            return Ok();
        }
    }
}