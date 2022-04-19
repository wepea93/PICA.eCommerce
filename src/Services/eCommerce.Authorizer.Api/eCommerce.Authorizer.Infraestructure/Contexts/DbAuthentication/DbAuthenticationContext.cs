using Authorizer.Core.Objects.DbTypes;
using Autorizer.Core.Config;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Authorizer.Infraestructure.Models.DbAuthentication
{
    public partial class DbAuthenticationContext : DbContext
    {
        private string _spCreateAccessToken { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:AuthenticationDatabase:StoreProcedures:CreateAccessToken"].ToString();
        private string _spValidatetAccessToken { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:AuthenticationDatabase:StoreProcedures:ValidateAccessToken"].ToString();
        private string _spGetAppUser { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:AuthenticationDatabase:StoreProcedures:GetAppUser"].ToString();


        public DbAuthenticationContext(DbContextOptions<DbAuthenticationContext> options) : base(options) { }

        public virtual DbSet<AppUserEntity> ApiUserEntity { get; set; }
        public virtual DbSet<AccessTokenEntity> AccessTokenEntity { get; set; }
        public virtual DbSet<ValidateAccessToken> ValidateAccessToken { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<bool> SpCreateAccessTokenAsync(AccessTokenEntity accessToken)
        {
            var result = await this.Database.ExecuteSqlRawAsync($"EXEC {_spCreateAccessToken} @appCode, @token, @expiresAt",
                new SqlParameter("@appCode", accessToken.AppCode),
                new SqlParameter("@token", accessToken.Token),
                new SqlParameter("@expiresAt", accessToken.ExpiresAt));
            return result > 0;
        }

        public async Task<bool> SpValidateAccessToken(string token, string appCodeOrigen, string appCodeDestiny)
        {
            var result = await this.Set<ValidateAccessToken>().FromSqlInterpolated($"{_spValidatetAccessToken} {token}, {appCodeOrigen}, {appCodeDestiny} ").ToListAsync();
            return result != null && result.Any() ? result.FirstOrDefault().IsValid : false;
        }

        public async Task<AppUserEntity> SpGetAppUsernAsync(string apiUser, string apiKey)
        {
            var result = await this.Set<AppUserEntity>().FromSqlInterpolated($"{_spGetAppUser} {apiUser}, {apiKey}").ToListAsync();
            return result != null && result.Any() ? result.FirstOrDefault() : null;
        }

    }
}
