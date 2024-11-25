using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Card;
using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Services.Interfaces.Cart
{
    public interface IPaymentService
    {
        Task<ServiceResponse> Pay(decimal amount,IEnumerable<Product> products,IEnumerable<ProcessCart> carts);
    }
}
