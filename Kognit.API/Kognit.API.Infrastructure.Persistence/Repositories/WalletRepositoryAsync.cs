using Kognit.API.Application.Features.Wallets.Queries.GetWallets;
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
    public class WalletRepositoryAsync : BaseRepositoryAsync<Wallet>, IWalletRepositoryAsync
    {
        private readonly DbSet<Wallet> _walletContext;

        public WalletRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Wallet> dataShaper, IModelHelper modelHelper) : base(dbContext, dataShaper, modelHelper)
        {
            _walletContext = dbContext.Set<Wallet>();
        }

        public async Task<(IEnumerable<DynamicEntity<Wallet>> data, RecordsCount recordsCount)> GetPaginatedWalletResponseAsync(
            GetWalletsQuery requestParams)
        {
            var userId = requestParams.UserId;
            var userName = requestParams.UserName;
            var bank = requestParams.BankName;

            var result = _walletContext.AsNoTracking().AsExpandable();
            var predicate = FilterByColumn(ref result, userId, userName, bank);

            return await GetPaginatedReponseAsync(requestParams, predicate);
        }

        private static Expression<Func<Wallet, bool>> FilterByColumn(
            ref IQueryable<Wallet> wallets, Guid userId, string userName, string bank)
        {
            var predicate = PredicateBuilder.New<Wallet>();

            if (!wallets.Any())
                return null;

            if (string.IsNullOrEmpty(bank) && string.IsNullOrEmpty(userName) && userId == Guid.Empty)
                return null;

            if (!string.IsNullOrEmpty(bank))
                predicate = predicate.Or(p => p.BankName.ToLower().Contains(bank.ToLower().Trim()));

            if (!string.IsNullOrEmpty(userName))
                predicate = predicate.Or(p => p.User.Name.ToLower().Contains(userName.ToLower().Trim()));

            if (userId != Guid.Empty)
                predicate = predicate.Or(p => p.UserId == userId);

            return predicate;
        }
    }
}