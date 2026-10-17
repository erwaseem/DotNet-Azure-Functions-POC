using AzureFunctionPOC.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionPOC.Application.Interfaces
{
    //public interface IProductService
    //{
    //    Task<ProductDto> GetProductAsync(int id);
    //}
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductsAsync();

        Task<ProductDto?> GetProductAsync(int id);

        Task<ProductDto> CreateProductAsync(ProductDto productDto);
    }
}
