using AutoMapper;
using Common.Enums.Response;
using Common.Utils.Extensions;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Resources.Dtos.ProductType;
using Resources.Dtos.Response;

namespace Services
{
    public class ProductTypeService : IProductTypeService
    {

        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;
        public ProductTypeService(IProductTypeRepository productTypeRepository, IMapper mapper)
        {
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        public async Task<ResultDto<ProductTypeDto>> GetProductTypeByIdAsync(Guid Id)
        {
            var result = new ResultDto<ProductTypeDto>();

            var productType = await _productTypeRepository.GetByIdAsync(Id);

            if (productType != null)
            {
                
                result = new ResultDto<ProductTypeDto>(_mapper.Map<ProductTypeDto>(productType));
            }
            else
            {
                result.AddError(ErrorTypeEnum.EntityNotExists, nameof(ErrorTypeEnum.EntityNotExists));
            }

            return result;
        }

        public async Task<ResultDto<IEnumerable<ProductTypeDto>>> GetAllProductTypesAsync()
        {
            var result = new ResultDto<IEnumerable<ProductTypeDto>>();

            var productTypes = await _productTypeRepository.GetAllAsync();

            if (productTypes != null)
            {
                result = new ResultDto<IEnumerable<ProductTypeDto>>(_mapper.Map<List<ProductTypeDto>>(productTypes));
            }
            else
            {
                result.AddError(ErrorTypeEnum.EntityNotExists, nameof(ErrorTypeEnum.EntityNotExists));
            }

            return result;
        }

        public async Task<ResultDto<bool>> CreateProductTypeAsync(ProductTypePostDto createDto)
        {
            var result = new ResultDto<bool>();

            var createResult = await _productTypeRepository.CreateAndSaveAsync(_mapper.Map<ProductTypePostDto, ProductType>(createDto));

            result = new ResultDto<bool>(createResult.IsOk);

            if (createResult.HasError)
            {
                result.AddError(ErrorTypeEnum.CreateError, createResult.Error!.ErrorMessage);
            }

            return result;
        }
        public async Task<ResultDto<bool>> UpdateProductTypeAsync(ProductTypePutDto updateDto)
        {
            var result = new ResultDto<bool>();

            var updateResult = await _productTypeRepository.UpdateAndSaveAsync(_mapper.Map<ProductType>(updateDto));

            result = new ResultDto<bool>(updateResult.IsOk);

            if (updateResult.HasError)
            {
                result.AddError(ErrorTypeEnum.UpdateError, updateResult.Error!.ErrorMessage);
            }

            return result;
        }

        public async Task<ResultDto<bool>> DeleteProductTypeAsync(Guid Id)
        {
            var result = new ResultDto<bool>();

            var productType = await _productTypeRepository.GetByIdAsync(Id);

            if (productType != null)
            {
                var deleteResult = await _productTypeRepository.DeleteAndSaveAsync(productType);

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
