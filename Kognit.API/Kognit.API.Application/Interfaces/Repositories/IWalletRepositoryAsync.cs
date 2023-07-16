using Kognit.API.Application.Features.Wallets.Queries.GetWallets;
using Kognit.API.Application.Parameters;
using Kognit.API.Domain.Common;
using Kognit.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kognit.API.Application.Interfaces.Repositories
{
    /// <summary>
    ///    Retorna uma lista de carteiras paginada após a aplicação dos filtros definidos na requisição.
    /// </summary>
    public interface IWalletRepositoryAsync : IRepositoryAsync<Wallet>
    {
        Task<(IEnumerable<DynamicEntity<Wallet>> data, RecordsCount recordsCount)> GetPaginatedWalletResponseAsync(GetWalletsQuery requestParameters);
    }
}