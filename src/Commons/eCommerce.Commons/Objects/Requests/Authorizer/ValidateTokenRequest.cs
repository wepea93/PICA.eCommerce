using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Objects.Requests.Authorizer
{
    public class ValidateTokenRequest
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Maximum 6 characters")]
        public string AppCode { get; set; }
    }
}
