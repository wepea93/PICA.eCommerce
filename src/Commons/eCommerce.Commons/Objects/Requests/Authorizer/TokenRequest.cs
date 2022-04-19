using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Objects.Requests.Authorizer
{
    public  class TokenRequest
    {
        [Required]
        public string ApiUser { get; set; }

        [Required]
        public string ApiKey { get; set; }
    }
}
