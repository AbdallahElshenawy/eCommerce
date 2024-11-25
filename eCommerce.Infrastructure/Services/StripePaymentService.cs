using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Card;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Services
{
    public class StripePaymentService : IPaymentService
    {
        public async Task<ServiceResponse> Pay(decimal amount, IEnumerable<Product> products, IEnumerable<ProcessCart> carts)
        {
            try {
                var lineItems = new List<SessionLineItemOptions>();
                foreach (var product in products)
                {
                    var pQuantity = carts.FirstOrDefault(x => x.ProductId == product.Id);
                    lineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "EGP",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = product.Name,
                                Description = product.Description,
                            },
                            UnitAmount = (long)(product.Price * 100),
                        },
                        Quantity = pQuantity!.Quantity,
                    }
                        );
                }
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = [],
                    LineItems = lineItems,
                    Mode = "payment",
                    SuccessUrl = "https://localhost:7077/paymentSuccess",
                    CancelUrl = "https://localhost:7077/paymentCancel"
                };
                var service = new SessionService();
                Session session = await service.CreateAsync(options);
                return new ServiceResponse(true, session.Url);
            }catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
            }
    }
}
