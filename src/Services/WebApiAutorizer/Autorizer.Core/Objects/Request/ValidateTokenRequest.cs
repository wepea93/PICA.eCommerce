using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorizer.Core.Objects.Request
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
