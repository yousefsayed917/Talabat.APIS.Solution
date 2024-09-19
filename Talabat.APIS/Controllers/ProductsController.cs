using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Eintites;
using Talabat.Core.Repositories;

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
            var product= await _productRepo.GetAllAsync();
            return Ok(product);
        }
        #endregion
        #region GetProductById
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>>GetProduct(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            return Ok(product);
        }
        #endregion
    }
}
