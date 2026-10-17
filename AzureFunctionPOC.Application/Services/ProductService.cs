using AzureFunctionPOC.Application.Interfaces;
using AzureFunctionPOC.Infrastructure.Entities;
using AzureFunctionPOC.Infrastructure.Interfaces;
using AzureFunctionPOC.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionPOC.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto?> GetProductAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return MapToDto(product);
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var products = await _repository.GetProductsAsync();

            return products.Select(MapToDto).ToList();
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto dto)
        {
            var entity = new Product
            {
                Name = dto.Name,
                Category = dto.Category,
                Description = dto.Description,
                Brand = dto.Brand,
                Price = dto.Price,
                Quantity = dto.Quantity,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            var result = await _repository.AddAsync(entity);

            return MapToDto(result);
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Description = product.Description,
                Brand = product.Brand,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }
    }
}
