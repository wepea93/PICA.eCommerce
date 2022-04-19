using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Objects.Requests.Products
{
    public class ProductReviewRequest
    {
        [Required]
        public long ProductCode { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Review { get; set; }
        [Required]
        public int Value { get; set; }
    }
}
