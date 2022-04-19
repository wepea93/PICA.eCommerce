using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorizer.Core.Objects.Dtos
{
    public class AccessTokenDto
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string ExpiresAt { get; set; }
        public bool IsTokenValid { get; set; }

        public AccessTokenDto(string userName, string accessToken, string expiresAt)
        {
            UserName = userName;
            AccessToken = accessToken;
            ExpiresAt = expiresAt;
            IsTokenValid = true;
        }

        public AccessTokenDto() 
        {
            IsTokenValid = false;
        }
    }
}
