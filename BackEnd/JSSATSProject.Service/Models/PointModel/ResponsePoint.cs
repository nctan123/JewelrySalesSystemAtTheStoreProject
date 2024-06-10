using JSSATSProject.Repository.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JSSATSProject.Service.Models.PointModel
{
    public class ResponsePoint
    {
      
        public int Id { get; set; }

        public int? AvailablePoint { get; set; }

        public int? Totalpoint { get; set; }

    }
}
