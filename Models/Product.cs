using System.ComponentModel.DataAnnotations;

namespace RestfullApiIntegration.Models
{
    public class Product
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name can't exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
        public string Description { get; set; } = "";
    }
}
