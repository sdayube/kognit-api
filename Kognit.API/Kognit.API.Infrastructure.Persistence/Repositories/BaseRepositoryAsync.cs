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
    public class BaseRepositoryAsync<T> : IRepositoryAsync<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDataShapeHelper<T> _dataShaper;
        private readonly IModelHelper _modelHelper;

        public BaseRepositoryAsync(ApplicationDbContext dbContext, IDataShapeHelper<T> dataShaper, IModelHelper modelHelper)
        {
            _dbContext = dbContext;
            _dataShaper = dataShaper;
            _modelHelper = modelHelper;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var fields = _modelHelper.GetModelFields<T>();
            return await _dbContext.Set<T>()
                .Select<T>("new(" + fields.Split(".")[0] + ")")
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<(IEnumerable<DynamicEntity<T>> data, RecordsCount recordsCount)> GetPaginatedReponseAsync(QueryParameter requestParams, Expression<Func<T, bool>> predicate)
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
                filteredResult = filteredResult.Select<T>("new(" + fields.Split(".")[0] + ")");
            }

            var resultData = await filteredResult
                .OrderBy($"{orderBy} {direction}")
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