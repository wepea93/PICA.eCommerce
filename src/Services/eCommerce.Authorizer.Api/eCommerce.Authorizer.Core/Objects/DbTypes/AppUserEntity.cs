using System.ComponentModel.DataAnnotations;


namespace Authorizer.Core.Objects.DbTypes
{
    public  class AppUserEntity
    {
        [Key]
        public string AppCode { get; set; }
        public string ApiUser { get; set; }

        public AppUserEntity() { }
    }
}
