using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Eintites;
using Talabat.Core.Repositories;
using Talabat.Core.Specification;

namespace Talabat.APIS.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenaricRepository<Product> _productRepo;

        public ProductsController(IGenaricRepository<Product> ProductRepo)
        {
            _productRepo = ProductRepo;
        }
        #region GetAllProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Spec = new ProductWithBrandAndTypeSpecification();
            var product = await _productRepo.GetAllWithSpecAsync(Spec);
            //var product= await _productRepo.GetAllAsync();
            return Ok(product);
        }
        #endregion
        #region GetProductById
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>>GetProduct(int id)
        {
           var Spec = new ProductWithBrandAndTypeSpecification(id);
            var product = await _productRepo.GetByIdWithSpecAsync(Spec);
            return Ok(product);
        }
        #endregion
    }
}
