using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.AccountModel
{
    public class ResponseToken
    {
        
        public string StaffId { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
