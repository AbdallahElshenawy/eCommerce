using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories.Cart
{
    public class CartRepository(AppDbContext context) : Icart
    {
        public async Task<int> SaveChecoutHistory(IEnumerable<Achieve> chechouts)
        {
            context.ChechoutAchieves.AddRange(chechouts);
            return await context.SaveChangesAsync();
        }
    }
}
