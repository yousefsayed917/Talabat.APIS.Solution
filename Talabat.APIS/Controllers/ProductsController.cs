using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Talabat.APIS.DTOS;
using Talabat.APIS.Errors;
using Talabat.Core.Eintites;
using Talabat.Core.Repositories;
using Talabat.Core.Specification;

namespace Talabat.APIS.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenaricRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenaricRepository<Product> ProductRepo,IMapper  mapper)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
        }
        #region GetAllProducts
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams Params)
        {
            var Spec = new ProductWithBrandAndTypeSpecification(Params);
            var product = await _productRepo.GetAllWithSpecAsync(Spec);
            var MappedProducts= _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(product);
            //var product= await _productRepo.GetAllAsync();
            return Ok(MappedProducts);
        }
        #endregion
        #region GetProductById
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>>GetProduct(int id)
        {
           var Spec = new ProductWithBrandAndTypeSpecification(id);
            var product = await _productRepo.GetByIdWithSpecAsync(Spec);
            if (product is null)
                return NotFound(new ApiResponse(404));
            var MappedProducts = _mapper.Map<Product,ProductToReturnDto>(product);
            return Ok(MappedProducts);
        }
        #endregion
    }
}