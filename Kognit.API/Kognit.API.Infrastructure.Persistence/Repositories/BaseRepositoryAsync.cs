using Kognit.API.Application.Interfaces;
using Kognit.API.Application.Parameters;
using Kognit.API.Domain.Common;
using Kognit.API.Infrastructure.Persistence.Contexts;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kognit.API.Infrastructure.Persistence.Repository
{
    public class BaseRepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private IDataShapeHelper<T> _dataShaper;

        public BaseRepositoryAsync(ApplicationDbContext dbContext, IDataShapeHelper<T> dataShaper)
        {
            _dbContext = dbContext;
            _dataShaper = dataShaper;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<(IEnumerable<DynamicEntity> data, RecordsCount recordsCount)> GetPaginatedReponseAsync(QueryParameter requestParams, Expression<Func<T, bool>> predicate)
        {
            var pageNumber = requestParams.PageNumber;
            var pageSize = requestParams.PageSize;
            var orderBy = requestParams.OrderBy ?? "id";
            var direction = requestParams.OrderDirection ?? "ASC";
            var fields = requestParams.Fields;

            var result = _dbContext.Set<T>().AsNoTracking().AsExpandable();
            var filteredResult = (predicate != null) ? result.Where(predicate) : result;

            var recordsCount = new RecordsCount
            {
                RecordsFiltered = await result.CountAsync(),
                RecordsTotal = await filteredResult.CountAsync()
            };

            if (!string.IsNullOrWhiteSpace(fields))
            {
                filteredResult = filteredResult.Select<T>("new(" + fields + ")");
            }

            var resultData = await filteredResult
                .OrderBy($"{orderBy} {direction}")
                .Select<T>("new(" + fields + ")")
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var shapeData = _dataShaper.ShapeData(resultData, fields);

            return (shapeData, recordsCount);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }
    }
}