﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTOs.Card
{
    public class GetPaymentMethod
    {
        public required Guid Id { get; set; } 
        public required string Name { get; set; } 
    }
}
