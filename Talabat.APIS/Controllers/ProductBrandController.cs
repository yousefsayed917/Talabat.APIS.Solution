using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Eintites;
using Talabat.Core.Repositories;

namespace Talabat.APIS.Controllers
{
    public class ProductBrandController : APIBaseController
    {
        private readonly IGenaricRepository<ProductBrand> _brandRepo;

        public ProductBrandController(IGenaricRepository<ProductBrand> BrandRepo)
        {
            _brandRepo = BrandRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var Brands = await _brandRepo.GetAllAsync();
            return Ok(Brands);
        }
    }
}
