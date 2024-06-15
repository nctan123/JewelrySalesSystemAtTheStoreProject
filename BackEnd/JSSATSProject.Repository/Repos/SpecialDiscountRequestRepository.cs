﻿using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Repository.Repos
{
    public class SpecialDiscountRequestRepository : GenericRepository<SpecialDiscountRequest>
    {
        public SpecialDiscountRequestRepository(DBContext context) : base(context)
        {
        }
    }
}