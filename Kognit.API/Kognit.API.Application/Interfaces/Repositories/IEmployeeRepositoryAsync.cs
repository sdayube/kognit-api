using Kognit.API.Application.Features.Employees.Queries.GetEmployees;
using Kognit.API.Application.Parameters;
using Kognit.API.Domain.Common;
using Kognit.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kognit.API.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interface for retrieving paged employee response asynchronously.
    /// </summary>
    /// <param name="requestParameters">The request parameters.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public interface IEmployeeRepositoryAsync : IRepositoryAsync<Employee>
    {
        Task<(IEnumerable<DynamicEntity> data, RecordsCount recordsCount)> GetPagedEmployeeResponseAsync(GetEmployeesQuery requestParameters);
    }
}