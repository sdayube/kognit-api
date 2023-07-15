using Kognit.API.Application.Parameters;
using Kognit.API.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kognit.API.Application.Interfaces
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<(IEnumerable<DynamicEntity> data, RecordsCount recordsCount)> GetPaginatedReponseAsync(QueryParameter requestParams, Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}