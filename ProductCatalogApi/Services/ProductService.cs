using ProductCatalogApi.DTOs;
using ProductCatalogApi.Interfaces;
using ProductCatalogApi.Models;
using ProductCatalogApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ProductDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            //return await _context.Products
            //    .Where(p => 
            //        (p.Price > 10000 && p.StockQuantity >= 10) || p.StockQuantity >= 50)
            //    .ToListAsync();

            //return await _context.Products
            //    .Where(p => p.Name.Contains("lap"))
            //    .ToListAsync();

            //return await _context.Products
            //    .Where(p => p.Name.EndsWith("Top"))
            //    .ToListAsync();

            //return await _context.Products
            //    .OrderByDescending(p => p.Price)
            //    .ToListAsync();

            //return await _context.Products
            //    .Where(p => p.Price > 10000)
            //    .OrderByDescending(p => p.Price)
            //    .ThenBy(p => p.StockQuantity)
            //    .ToListAsync();

            //var products = await _context.Products
            //    .Where(p => p.Price > 10000)
            //    .Select(p => new
            //    {
            //        p.Name,
            //        p.Price
            //    })
            //    .OrderByDescending(p => p.Price)
            //    .ToListAsync();

            //var products = await _context.Products
            //    .Where(p => p.StockQuantity >= 10)
            //    .Select(p => new
            //    {
            //        p.Name,
            //        p.StockQuantity
            //    })
            //    .OrderByDescending(p => p.StockQuantity)
            //    .ToListAsync();

            //return await _context.Products
            //    .FirstOrDefaultAsync(p => p.StockQuantity > 55);

            //return await _context.Products
            //    .Where(p => p.StockQuantity < 50)
            //    .OrderByDescending(p => p.StockQuantity)
            //    .FirstOrDefaultAsync();

            //return await _context.Products
            //    .SingleOrDefaultAsync(p => p.Name == "Laptop");

            //return await _context.Products
            //    .SingleOrDefaultAsync(p => p.StockQuantity == 50);

            //return await _context.Products
            //    .AnyAsync(p => p.StockQuantity == 60);

            //return await _context.Products
            //    .CountAsync(p => p.Price > 10000);

            //var sumOfQuanity = await _context.Products
            //    .Where(p => p.Price > 10000)
            //    .SumAsync(p => p.StockQuantity);

            //return sumOfQuanity;

            //var average = await _context.Products
            //    .Where(p => p.StockQuantity > 100)
            //    .AverageAsync(p => p.Price);

            //return average;

            //var cheapestPrice = await _context.Products
            //    .Where(p => p.StockQuantity >= 100)
            //    .MinAsync(p => p.Price);

            //return cheapestPrice;

            //return await _context.Products
            //    .Select(p => p.Price)
            //    .Distinct()
            //    .ToListAsync();

            //return await _context.Products
            //    .OrderByDescending(p => p.Price)
            //    .Skip(2)
            //    .Take(2)
            //    .ToListAsync();

            // Eager loading
            //return await _context.Products
            //    .Include(p => p.Category)
            //    .ToListAsync();

            //var products = await _context.Products
            //    .ToListAsync();

            //foreach (var product in products)
            //{
            //    //Explicit loading
            //    await _context.Entry(product)
            //        .Reference(p => p.Category)
            //        .LoadAsync();
            //}

            //return products;

            //return await _context.Categories
            //    .Include(c => c.Products
            //        .Where(p => p.Price > 200000))
            //    .FirstOrDefaultAsync(c => c.Name == "Electronics");

            //return await _context.Categories
            //    .Include(c => c.Products)
            //    .ThenInclude(p => p.Supplier)
            //    .ToListAsync();

            //return await _context.Categories
            //    .Where(c => c.Products.Any(p => p.Price > 200000))
            //    .ToListAsync();

            return await _context.Categories
                .Where(c => c.Products.All(p => p.Price > 5000))
                .ToListAsync();
        }

        public async Task<List<ProductDto>> GetProductsAsync(int page, int pageSize)
        {
            //var products = await _context.Products
            //    .OrderBy(p => p.Id)
            //    .Skip(2)
            //    .Take(2)
            //    .ToListAsync();

            //return products;

            if (page < 1)
                throw new ArgumentException("Page must be at least 1.");

            if (pageSize < 1)
                throw new ArgumentException("Page size must be at least 1.");

            var products = await _context.Products
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity
                })
                .ToListAsync();

            return products;
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Product {ProductId} created successfully", product.Id);

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };
        }
    }
}