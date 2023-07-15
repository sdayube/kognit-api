using Kognit.API.Application.Features.Users.Queries.GetUsers;
using Kognit.API.Application.Interfaces;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Parameters;
using Kognit.API.Domain.Common;
using Kognit.API.Domain.Entities;
using Kognit.API.Infrastructure.Persistence.Contexts;
using Kognit.API.Infrastructure.Persistence.Repository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kognit.API.Infrastructure.Persistence.Repositories
{
    public class UserRepositoryAsync : BaseRepositoryAsync<User>, IUserRepositoryAsync
    {
        private readonly DbSet<User> _userContext;
        private readonly IMockService _mockData;

        public UserRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<User> dataShaper, IMockService mockData) : base(dbContext, dataShaper)
        {
            _userContext = dbContext.Set<User>();
            _mockData = mockData;
        }

        public async Task<bool> IsUniqueCpfAsync(string cpf)
        {
            return await _userContext
                .AllAsync(p => p.CPF != cpf);
        }

        public async Task<(IEnumerable<DynamicEntity<User>> data, RecordsCount recordsCount)> GetPaginatedUserReponseAsync(GetUsersQuery requestParams)
        {
            var userName = requestParams.Name;
            var userCpf = requestParams.CPF;
            var userBirthDate = requestParams.BirthDate;

            var result = _userContext.AsNoTracking().AsExpandable();
            var predicate = FilterByColumn(result, userName, userCpf, userBirthDate);

            return await GetPaginatedReponseAsync(requestParams, predicate);
        }

        private static Expression<Func<User, bool>> FilterByColumn(IQueryable<User> users, string userName, string userCpf, DateTime? userBirthDate)
        {
            var predicate = PredicateBuilder.New<User>();

            if (!users.Any())
                return null;

            if (string.IsNullOrEmpty(userCpf) && string.IsNullOrEmpty(userName) && userBirthDate == null)
                return null;


            if (!string.IsNullOrEmpty(userName))
                predicate = predicate.Or(p => p.Name.Contains(userName.Trim()));

            if (!string.IsNullOrEmpty(userCpf))
                predicate = predicate.Or(p => p.CPF.Contains(userCpf.Trim()));

            if (userBirthDate != null)
            {
                DateTime birthDate = userBirthDate.Value.Date;
                DateTime nextDay = birthDate.AddDays(1);
                predicate = predicate.Or(p => p.BirthDate >= birthDate && p.BirthDate < nextDay);
            }

            return predicate;
        }
    }
}