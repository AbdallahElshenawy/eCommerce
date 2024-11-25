using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Specifications
{
    public class ProducctWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProducctWithTypesAndBrandsSpecification()
        {
            AddIncludes(x => x.productType);
            AddIncludes(x => x.productBrand);
        }
        public ProducctWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddIncludes(x => x.productType);
            AddIncludes(x => x.productBrand);
        }
    }
}
