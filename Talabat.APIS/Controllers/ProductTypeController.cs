using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Eintites;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.APIS.Controllers
{
    
    public class ProductTypeController : APIBaseController
    {
        private readonly IGenaricRepository<ProductType> _typeRepo;

        public ProductTypeController(IGenaricRepository<ProductType> TypeRepo)
        {
            _typeRepo = TypeRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _typeRepo.GetAllAsync();
            return Ok(types);
        }
    }
}
