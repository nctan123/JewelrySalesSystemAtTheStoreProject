using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JSSATSProject.Service.Models.PointModel
{
    public class RequestCreatePoint
    {
       
        public int? AvailablePoint { get; set;}
        public int? TotalPoints { get; set;}

    }
}
