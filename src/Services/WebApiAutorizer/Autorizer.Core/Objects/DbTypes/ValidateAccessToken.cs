using System.ComponentModel.DataAnnotations;

namespace Authorizer.Core.Objects.DbTypes
{
    public class ValidateAccessToken
    {
        [Key]
        public bool IsValid { get; set; }
    }
}
