using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Eintites;

namespace Talabat.Core.Specification
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductSpecParams Params) 
            :base(p=>(!Params.BrandId.HasValue||p.ProductBrandId== Params.BrandId) &&(!Params.TypeId.HasValue||p.ProductTypeId==Params.TypeId))
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
            if (!string.IsNullOrEmpty(Params.sort))
            {
                switch(Params.sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                        default:
                        AddOrderBy(p=>p.Name);
                        break;
                }
                ApplyPagination(Params.PageSize*(Params.PageIndex-1), Params.PageSize);
            }
        }
        public ProductWithBrandAndTypeSpecification(int id) : base(p=>p.Id==id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
