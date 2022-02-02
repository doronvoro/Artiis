using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atriis.ProductManagement.BL
{
    public class ProductNameFilter : ISpecification<Product>
    {
        private string _name;

        public ProductNameFilter(string name)
        {
            _name = name;
        }
        public bool IsSatisfied(Product  product)
        {
           if(product == null)
            {
                return false;
            }

           var result = (product.Name ?? string.Empty).Contains(_name ?? string.Empty);
            return result;
        }
    }

    public class InMemoryFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }

}
