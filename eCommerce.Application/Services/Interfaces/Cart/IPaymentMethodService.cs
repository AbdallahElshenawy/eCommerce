using eCommerce.Application.DTOs.Card;
namespace eCommerce.Application.Services.Interfaces.Cart
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<GetPaymentMethod>> GetPaymentMethods();
    }
}
