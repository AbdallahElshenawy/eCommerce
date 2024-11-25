using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Card;

namespace eCommerce.Application.Services.Interfaces.Cart
{
    public interface ICartService
    {
        Task<ServiceResponse> SaveChecoutHistory(IEnumerable<CreateAchieve> achieves);
        Task<ServiceResponse> Checkout(Checkout checkout);

    }
}
