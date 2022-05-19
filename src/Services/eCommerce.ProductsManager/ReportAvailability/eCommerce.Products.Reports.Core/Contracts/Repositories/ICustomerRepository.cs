﻿using eCommerce.Products.Reports.Core.Objects.DbTypes;

namespace eCommerce.Products.Reports.Core.Contracts.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerEntity>?> GetCustomersByIdAsync(IEnumerable<CustomerEntity> customerEntities);
    }
}
