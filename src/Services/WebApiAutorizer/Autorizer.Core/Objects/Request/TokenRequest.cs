using System.ComponentModel.DataAnnotations;

namespace Authorizer.Core.Objects.Request
{
    public  class TokenRequest
    {
        [Required]
        public string ApiUser { get; set; }

        [Required]
        public string ApiKey { get; set; }
    }
}
