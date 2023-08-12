using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailApp.Model
{
    public class Product
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; }

        //[Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public DateTime PostedDate { get; set; }
    }
}
