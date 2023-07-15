using Kognit.API.Application.Features.Users.Queries.GetUsers;
using Kognit.API.Application.Parameters;
using Kognit.API.Domain.Common;
using Kognit.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kognit.API.Application.Interfaces.Repositories
{
    public interface IUserRepositoryAsync : IRepositoryAsync<User>
    {
        /// <summary>
        ///     Valida se o CPF é único.
        /// </summary>
        /// <param name="cpf">Número do CPF sem caracteres especiais</param>
        Task<bool> IsUniqueCpfAsync(string cpf);

        /// <summary>
        ///    Retorna uma lista de usuários paginada após a aplicação dos filtros definidos na requisição.
        /// </summary>
        Task<(IEnumerable<DynamicEntity<User>> data, RecordsCount recordsCount)> GetPaginatedUserReponseAsync(GetUsersQuery requestParameters);
    }
}