using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorizer.Core.Objects.DbTypes
{
    public class AccessTokenEntity
    {
        [Key]
        public long Id { get; set; }
        public string AppCode { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public AccessTokenEntity(string appCode, string accessToken, DateTime expiresAt)
        {
            AppCode = appCode;
            Token = accessToken;
            ExpiresAt = expiresAt;
        }

        public AccessTokenEntity() { }
    }
}
