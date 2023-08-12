using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RetailApp.DTO;
using RetailApp.Interfaces;
using RetailApp.Model;
using System.Xml.Linq;

namespace Retail.Models
{
    public class RetailDbContext : DbContext, IRetailDbContext
    {
        public RetailDbContext(DbContextOptions<RetailDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApprovalQueue>()
                .HasKey(a => a.ApprovalId);

            modelBuilder.Entity<Product>()
                .HasKey(a => a.ProductId);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ApprovalQueue> ApprovalQueue { get; set; }


        
       public int UpdateProductWithApproval(int productId, ProductDTO productDTO)
        {
            var result = Database.ExecuteSqlRaw("EXEC dbo.UpdateProductWithApproval @ProductId, @Name, @Price, @Status",
                new SqlParameter("@ProductId", productId),
                new SqlParameter("@Name", productDTO.Name),
                new SqlParameter("@Price", productDTO.Price),
                new SqlParameter("@Status", productDTO.Status));
                
                return result;
        }
    }
}
