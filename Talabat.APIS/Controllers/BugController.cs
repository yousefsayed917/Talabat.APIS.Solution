using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIS.Controllers
{
    public class BugController : APIBaseController
    {
        private readonly TalabatDbContext _dbContext;

        public BugController(TalabatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {
            var Product = _dbContext.Products.Find(100);
            if (Product == null) 
                return NotFound();
            return Ok(Product);
        }
        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var Product = _dbContext.Products.Find(100);
            var ProductToReturn = Product.ToString();
            return Ok(Product);
            //Null Reference Exception
        }
        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("BadRequest/{Id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }

    }
}
