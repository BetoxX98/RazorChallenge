using Resources.Dtos.Product;
using Resources.Dtos.Response;

namespace Infrastructure.Interfaces.Services
{
    public interface IProductService
    {
        Task<ResultDto<ProductDto>> GetProductByIdAsync(Guid Id);
        Task<ResultDto<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<ResultDto<bool>> CreateProductAsync(ProductPostDto Dto);
        Task<ResultDto<bool>> UpdateProductAsync(ProductPutDto Dto);
        Task<ResultDto<bool>> DeleteProductAsync(Guid Id);
    }
}
