using System.ComponentModel.DataAnnotations;

namespace Products.Core.Objects.Request
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
