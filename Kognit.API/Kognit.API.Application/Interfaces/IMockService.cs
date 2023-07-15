using Kognit.API.Domain.Entities;
using System.Collections.Generic;

namespace Kognit.API.Application.Interfaces
{
    public interface IMockService
    {
        List<User> GetPositions(int rowCount);

        List<Employee> GetEmployees(int rowCount);

        /// <summary>
        ///     Gera uma lista de usuários com dados randomizados.
        /// </summary>
        /// <param name="rowCount">Quantidade de entradas a serem geradas.</param>
        List<User> SeedUsers(int rowCount);
    }
}