using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Resources.Dtos.Product;
using Resources.Dtos.Response;
using Domain.Entities;
using AutoMapper;
using Common.Enums.Response;
using Common.Utils.Extensions;


namespace Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResultDto<ProductDto>> GetProductByIdAsync(Guid Id)
        {
            var result = new ResultDto<ProductDto>();

            var product = await _productRepository.GetByIdAsync(Id);

            if (product != null)
            {
                result = new ResultDto<ProductDto>(_mapper.Map<Product, ProductDto>(product));
            }
            else
            {
                result.AddError(ErrorTypeEnum.EntityNotExists, nameof(ErrorTypeEnum.EntityNotExists));
            }

            return result;
        }

        public async Task<ResultDto<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            var result = new ResultDto<IEnumerable<ProductDto>>();

            var products = await _productRepository.GetAllAsync();

            if (products.NotNullOrEmpty())
            {
                result = new ResultDto<IEnumerable<ProductDto>>(_mapper.Map<IEnumerable<Product>, List<ProductDto>>(products));
            }
            else
            {
                result.AddError(ErrorTypeEnum.EntityNotExists, nameof(ErrorTypeEnum.EntityNotExists));
            }

            return result;
        }

        public async Task<ResultDto<bool>> CreateProductAsync(ProductPostDto createDto)
        {
            var result = new ResultDto<bool>();

            var createResult = await _productRepository.CreateAndSaveAsync(_mapper.Map<ProductPostDto, Product>(createDto));

            result = new ResultDto<bool>(createResult.IsOk);

            if (createResult.HasError)
            {
                result.AddError(ErrorTypeEnum.CreateError, createResult.Error!.ErrorMessage);
            }

            return result;
        }
        public async Task<ResultDto<bool>> UpdateProductAsync(ProductPutDto updateDto)
        {
            var result = new ResultDto<bool>();

            var updateResult = await _productRepository.UpdateAndSaveAsync(_mapper.Map<ProductPutDto, Product>(updateDto));

            result = new ResultDto<bool>(updateResult.IsOk);

            if (updateResult.HasError)
            {
                result.AddError(ErrorTypeEnum.UpdateError, updateResult.Error!.ErrorMessage);
            }

            return result;
        }

        public async Task<ResultDto<bool>> DeleteProductAsync(Guid Id)
        {
            var result = new ResultDto<bool>();

            var product = await _productRepository.GetByIdAsync(Id);

            if (product != null)
            {
                var deleteResult = await _productRepository.DeleteAndSaveAsync(product);
                
                result = new ResultDto<bool>(deleteResult.IsOk);

                if (deleteResult.HasError)
                {
                    result.AddError(ErrorTypeEnum.DeleteError, deleteResult.Error!.ErrorMessage);
                }
            }
            else
            {
                result.AddError(ErrorTypeEnum.EntityNotExists, nameof(ErrorTypeEnum.EntityNotExists));
            }

            return result;
        }

    }
}
