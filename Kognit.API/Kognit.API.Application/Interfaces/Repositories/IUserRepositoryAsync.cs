using Kognit.API.Application.Features.Users.Queries.GetUsers;
using Kognit.API.Application.Parameters;
using Kognit.API.Domain.Common;
using Kognit.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kognit.API.Application.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for Position entity with asynchronous methods.
    /// </summary>
    /// <param name="positionNumber">Position number to check for uniqueness.</param>
    /// <returns>
    /// Task indicating whether the position number is unique.
    /// </returns>
    /// <param name="rowCount">Number of rows to seed.</param>
    /// <returns>
    /// Task indicating the completion of seeding.
    /// </returns>
    /// <param name="requestParameters">Parameters for the query.</param>
    /// <param name="data">Data to be returned.</param>
    /// <param name="recordsCount">Number of records.</param>
    /// <returns>
    /// Task containing the paged response.
    /// </returns>    
    public interface IUserRepositoryAsync : IRepositoryAsync<User>
    {
        Task<bool> IsUniqueCpfAsync(string positionNumber);

        Task<bool> SeedDataAsync(int rowCount);

        Task<(IEnumerable<DynamicEntity> data, RecordsCount recordsCount)> GetPaginatedUserReponseAsync(GetUsersQuery requestParameters);
    }
}