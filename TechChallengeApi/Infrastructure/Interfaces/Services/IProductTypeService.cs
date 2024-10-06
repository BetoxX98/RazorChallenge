using Resources.Dtos.ProductType;
using Resources.Dtos.Response;

namespace Infrastructure.Interfaces.Services
{
    public interface IProductTypeService
    {
        Task<ResultDto<ProductTypeDto>> GetProductTypeByIdAsync(Guid Id);
        Task<ResultDto<IEnumerable<ProductTypeDto>>> GetAllProductTypesAsync();
        Task<ResultDto<bool>> CreateProductTypeAsync(ProductTypePostDto Dto);
        Task<ResultDto<bool>> UpdateProductTypeAsync(ProductTypePutDto Dto);
        Task<ResultDto<bool>> DeleteProductTypeAsync(Guid Id);
    }
}
