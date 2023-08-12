namespace RetailApp.Model
{
    public class ApprovalQueue
    {
        public int ApprovalId { get; }

        public int ProductId { get; set; }
        
        public Product? Product { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
