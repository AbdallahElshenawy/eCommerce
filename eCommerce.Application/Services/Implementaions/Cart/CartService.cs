using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Card;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Services.Implementaions.Cart
{
    public class CartService(Icart icart,IMapper mapper,IGeneric<Product>IProduct
        ,IPaymentMethodService paymentMethodService, IPaymentService paymentService) : ICartService
    {
        public async Task<ServiceResponse> Checkout(Checkout checkout)
        {
            var (products, totalAmount) = await GetCartTotalAmount(checkout.Carts);
            var paymentMethods= await paymentMethodService.GetPaymentMethods();
            if(checkout.PaymentMethodId==paymentMethods.FirstOrDefault()!.Id)
            {
                return await paymentService.Pay(totalAmount, products, checkout.Carts);
            }
            return new ServiceResponse(false, "Inva;id payment method");
        }

        public async Task<ServiceResponse> SaveChecoutHistory(IEnumerable<CreateAchieve> achieves)
        {
           var mapData= mapper.Map<IEnumerable<Achieve>>(achieves);
            var result = await icart.SaveChecoutHistory(mapData);
            return result > 0 ? new ServiceResponse(true, "Checkout achieved") : new ServiceResponse(false, "Error occurred in saving");
        }
        async Task<(IEnumerable<Product>,decimal)> GetCartTotalAmount(IEnumerable<ProcessCart> carts)
        {
            if (carts is null) return ([], 0);
            var products = await IProduct.GetAllAsync();
            if (products is null) return ([], 0);
            var cartProducts = carts.Select(cartItem=>products.FirstOrDefault(p=>p.Id==cartItem.ProductId))
                .Where(product=>product!=null).ToList();

            var totalAmount = carts.Where(cartItem => cartProducts.Any(p => p!.Id == cartItem.ProductId))
                .Sum(cartItem => cartItem.Quantity * cartProducts.First(p => p!.Id == cartItem.ProductId)!.Price);
            return(cartProducts!, totalAmount);


        }
    }
}
