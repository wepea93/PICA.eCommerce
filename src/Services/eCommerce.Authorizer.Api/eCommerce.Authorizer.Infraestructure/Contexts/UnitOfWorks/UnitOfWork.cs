using Authorizer.Infraestructure.Models.DbAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorizer.Infraestructure.Models.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbAuthenticationContext _dbContext;

        public UnitOfWork(DbAuthenticationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Confirm()
        {
            _dbContext.SaveChanges();
        }

        public async Task ConfirmAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
