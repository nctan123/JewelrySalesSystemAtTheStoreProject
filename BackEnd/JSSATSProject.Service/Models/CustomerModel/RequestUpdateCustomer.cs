using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.CustomerModel
{
    public class RequestUpdateCustomer
    {
        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }


    }
}
