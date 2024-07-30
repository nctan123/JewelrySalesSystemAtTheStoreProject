using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Repository.CustomValidors
{
    public interface IDateRange
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
