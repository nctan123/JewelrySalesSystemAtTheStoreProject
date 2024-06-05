using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.StaffModel
{
    public class RequestUpdateStaff
    {
        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Address { get; set; }

        public string? Gender { get; set; }

        public string? Status { get; set; }
    }
}
