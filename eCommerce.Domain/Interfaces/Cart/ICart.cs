using eCommerce.Domain.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces.Cart
{
    public interface Icart
    {
        Task<int> SaveChecoutHistory(IEnumerable<Achieve> chechouts);
    }
}
