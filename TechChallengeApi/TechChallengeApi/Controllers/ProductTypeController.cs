using Infrastructure.Interfaces.Entities;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Resources.Dtos.ProductType;
using Resources.Dtos.Response;
using TechChallengeApi.Controllers.Base;

namespace TechChallengeApi.Controllers
{
    [ControllerName("ProductType")]
    [Route("api/product-types")]
    public class ProductTypeController : BaseController
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(ILogger<ProductTypeController> logger,
                                 IHttpContextAccessor httpContextAccessor,
                                 IUser user,
                                 IProductTypeService productTypeService) 
                                 : base(logger, user, httpContextAccessor)
        {
            _productTypeService = productTypeService;
        }

        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(typeof(ResultDto<ProductTypeDto>), 200)]
        [ProducesResponseType(typeof(ResultDto<ProductTypeDto>), 404)]
        public async Task<IActionResult> GetProductTypeById(Guid Id)
        {
            Logger.LogInformation($"{nameof(ProductTypeController)}.{nameof(GetAllProductTypes)}");
            var Result = await _productTypeService.GetProductTypeByIdAsync(Id);

            return Response(Result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<ProductTypeDto>>), 200)]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<ProductTypeDto>>), 404)]
        public async Task<IActionResult> GetAllProductTypes()
        {
            Logger.LogInformation($"{nameof(ProductTypeController)}.{nameof(GetAllProductTypes)}");
            var Result = await _productTypeService.GetAllProductTypesAsync();

            return Response(Result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<bool>>), 200)]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<bool>>), 404)]
        public async Task<IActionResult> CreateProductType([FromBody] ProductTypePostDto Dto)
        {
            Logger.LogInformation($"{nameof(ProductTypeController)}.{nameof(CreateProductType)}");
            var Result = await _productTypeService.CreateProductTypeAsync(Dto);

            return Response(Result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<bool>>), 200)]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<bool>>), 404)]
        public async Task<IActionResult> UpdateProductType([FromBody] ProductTypePutDto Dto)
        {
            Logger.LogInformation($"{nameof(ProductTypeController)}.{nameof(UpdateProductType)}");
            var Result = await _productTypeService.UpdateProductTypeAsync(Dto);

            return Response(Result);
        }

        [HttpDelete]
        [Route("{Id}")]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<bool>>), 200)]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<bool>>), 404)]
        public async Task<IActionResult> DeleteProductType(Guid Id)
        {
            Logger.LogInformation($"{nameof(ProductTypeController)}.{nameof(DeleteProductType)}");
            var Result = await _productTypeService.DeleteProductTypeAsync(Id);

            return Response(Result);
        }
    }
}
