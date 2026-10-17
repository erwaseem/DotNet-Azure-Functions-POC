using AzureFunctionPOC.Infrastructure.Entities;
using AzureFunctionPOC.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionPOC.Infrastructure.Interfaces
{
    //public interface IProductRepository
    //{
    //    Task<ProductDto> GetProductAsync(int id);
    //}
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<Product> AddAsync(Product product);
    }
}
