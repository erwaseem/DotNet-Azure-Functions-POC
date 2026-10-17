using AzureFunctionPOC.Infrastructure.Data;
using AzureFunctionPOC.Infrastructure.Entities;
using AzureFunctionPOC.Infrastructure.Interfaces;
using AzureFunctionPOC.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionPOC.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return product;
        }
    }
    //public class ProductRepository : IProductRepository
    //{
    //    public async Task<ProductDto> GetProductAsync(int id)
    //    {
    //        return new ProductDto
    //        {
    //            Id = id,
    //            Name = "Laptop",
    //            Price = 50000
    //        };
    //    }
    //}
}
