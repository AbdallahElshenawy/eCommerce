﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTOs.Card
{
    public class CreateAchieve
    {
        [Required] 
        public Guid ProductId { get; set; }
        [Required]

        public int Quantity { get; set; }
        [Required]

        public Guid UserId { get; set; }
    }
}
