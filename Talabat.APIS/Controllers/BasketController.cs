using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.Core.Eintites;
using Talabat.Core.Repositories;

namespace Talabat.APIS.Controllers
{
    
    public class BasketController : APIBaseController
    {
        private readonly IBasketRepository _basketRepo;

        public BasketController(IBasketRepository basketRepo)
        {
            _basketRepo = basketRepo;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId)
        {
            var Basket = await _basketRepo.GetBasketAsync(BasketId);
            return Basket is null ? new CustomerBasket(BasketId) : Ok(Basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>>UpdateBasket(CustomerBasket basket)
        {
            var CreatedOrUpdated=await _basketRepo.UpdateBasketAsync(basket);
            if (CreatedOrUpdated is null) return BadRequest(new ApiResponse(400));
            return Ok(CreatedOrUpdated);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>>DeleteBasket(string BasketId)
        {
            return await _basketRepo.DeleteBasketAsync(BasketId);
        }

    }
}
