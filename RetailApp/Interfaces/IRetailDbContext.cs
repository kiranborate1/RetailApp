using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RetailApp.DTO;
using RetailApp.Model;

namespace RetailApp.Interfaces
{
    public interface IRetailDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<ApprovalQueue> ApprovalQueue { get; set; }
        public int UpdateProductWithApproval(int productId, ProductDTO productDTO);
        int SaveChanges();
    }
}
