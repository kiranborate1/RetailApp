using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Retail.Models;
using RetailApp.DTO;
using RetailApp.Interfaces;
using RetailApp.Model;

namespace RetailApp.Entities
{
    public class ProductService : IProductService
    {
        private readonly IRetailDbContext _dbContext;

        public ProductService(IRetailDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //all select
        public async Task<IEnumerable<ProductDTO>> GetActiveProducts()
        {
            return await _dbContext.Products
                .Where(p => p.Status)
                .OrderByDescending(p => p.PostedDate)
                .ThenByDescending(p => p.ProductId)
                .Select(p => new ProductDTO
                {
                    //ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    Status = p.Status,
                    PostedDate = p.PostedDate
                })
                .ToListAsync();
        }
        //search 
        public IEnumerable<Product> SearchProducts(string productName, decimal? minPrice, decimal? maxPrice, DateTime? minPostedDate, DateTime? maxPostedDate)
        {
            var query = _dbContext.Products.Where(p => p.Status);

            if (!string.IsNullOrEmpty(productName))
                query = query.Where(p => p.Name.Contains(productName));

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice);

            if (minPostedDate.HasValue)
                query = query.Where(p => p.PostedDate >= minPostedDate);

            if (maxPostedDate.HasValue)
                query = query.Where(p => p.PostedDate <= maxPostedDate);

            return query.OrderByDescending(p => p.PostedDate).ToList();
        }
        //insert entry
        public Product CreateProduct(Product product)
        {
             if (product.Price > 5000)
            {
                product.Status = false; 
                _dbContext.ApprovalQueue.Add(new ApprovalQueue { Product = product,ProductId=product.ProductId, RequestDate = DateTime.UtcNow });
            }
            else
            {
                _dbContext.Products.Add(product);
            }

            _dbContext.SaveChanges();
            return product;
        }
        //update entry
        public int UpdateProductWithApproval(int productId,ProductDTO productDTO)
        {
            return _dbContext.UpdateProductWithApproval(productId,productDTO);
        }

        //Delete and PUSH t Approval Queue
        public void DeleteProductAndPushToApproval(int productId)
        {
            var product = _dbContext.Products.Find(productId);

            if (product != null)
            {
                _dbContext.ApprovalQueue.Add(new ApprovalQueue
                {
                    ProductId=productId,
                    RequestDate = DateTime.UtcNow
                });

                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<ApprovalQueueDTO> GetProductsInApprovalQueue()
        {
            return _dbContext.ApprovalQueue
                .OrderBy(a => a.RequestDate)
                .Select(a => new ApprovalQueueDTO
                {
                    ProductId = a.ProductId,
                    RequestDate = a.RequestDate,
                })
                .ToList();
        }
    }
}
