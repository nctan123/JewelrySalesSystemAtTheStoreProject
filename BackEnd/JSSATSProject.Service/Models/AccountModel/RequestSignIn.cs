using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.AccountModel
{
    public class RequestSignIn
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
