using AutoMapper;
using eCommerce.Application.DTOs.Card;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Interfaces.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Services.Implementaions.Cart
{
    public class PaymentMethodService(IPaymentMethod paymentMethod,IMapper mapper) : IPaymentMethodService
    {
        public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethods()
        {
            var methods = await paymentMethod.GetPaymentMethods();
            if (!methods.Any()) return [];
            return mapper.Map<IEnumerable<GetPaymentMethod>>(methods);
        }
    }
}
