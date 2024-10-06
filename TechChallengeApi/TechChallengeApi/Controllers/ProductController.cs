using Infrastructure.Interfaces.Entities;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Resources.Dtos.Product;
using Resources.Dtos.Response;
using TechChallengeApi.Controllers.Base;

namespace TechChallengeApi.Controllers
{
    [ControllerName("Product")]
    [Route("api/products")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger,
                                 IHttpContextAccessor httpContextAccessor,
                                 IUser user,
                                 IProductService productService) 
                                 : base(logger, user, httpContextAccessor)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(typeof(ResultDto<ProductDto>), 200)]
        [ProducesResponseType(typeof(ResultDto<ProductDto>), 404)]
        public async Task<IActionResult> GetProductById(Guid Id)
        {
            Logger.LogInformation($"{nameof(ProductController)}.{nameof(GetAllProducts)}");
            var Result = await _productService.GetProductByIdAsync(Id);

            return Response(Result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<ProductDto>>), 200)]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<ProductDto>>), 404)]
        public async Task<IActionResult> GetAllProducts()
        {
            Logger.LogInformation($"{nameof(ProductController)}.{nameof(GetAllProducts)}");
            var Result = await _productService.GetAllProductsAsync();

            return Response(Result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<bool>>), 200)]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<bool>>), 404)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductPostDto Dto)
        {
            Logger.LogInformation($"{nameof(ProductController)}.{nameof(CreateProduct)}");
            var Result = await _productService.CreateProductAsync(Dto);

            return Response(Result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<bool>>), 200)]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<bool>>), 404)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductPutDto Dto)
        {
            Logger.LogInformation($"{nameof(ProductController)}.{nameof(UpdateProduct)}");
            var Result = await _productService.UpdateProductAsync(Dto);

            return Response(Result);
        }

        [HttpDelete]
        [Route("{Id}")]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<bool>>), 200)]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<bool>>), 404)]
        public async Task<IActionResult> DeleteProduct(Guid Id)
        {
            Logger.LogInformation($"{nameof(ProductController)}.{nameof(DeleteProduct)}");
            var Result = await _productService.DeleteProductAsync(Id);

            return Response(Result);
        }
    }
}
