using RetailApp.DTO;
using RetailApp.Model;

namespace RetailApp.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetActiveProducts();
        IEnumerable<Product> SearchProducts(string productName, decimal? minPrice, decimal? maxPrice, DateTime? minPostedDate, DateTime? maxPostedDate);
        Product CreateProduct(Product product);
        int UpdateProductWithApproval(int productId,ProductDTO productDTO);
        public void DeleteProductAndPushToApproval(int productId);
        public IEnumerable<ApprovalQueueDTO> GetProductsInApprovalQueue();
    }
}
