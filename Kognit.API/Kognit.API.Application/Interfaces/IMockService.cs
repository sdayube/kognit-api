using Kognit.API.Domain.Entities;
using System.Collections.Generic;

namespace Kognit.API.Application.Interfaces
{
    public interface IMockService
    {
        /// <summary>
        ///     Gera uma lista de carteiras com dados randomizados.
        /// </summary>
        /// <param name="rowCount">Quantidade de entradas a serem geradas.</param>
        List<Wallet> SeedWallets(int rowCount, IEnumerable<User> users);

        /// <summary>
        ///     Gera uma lista de usuários com dados randomizados.
        /// </summary>
        /// <param name="rowCount">Quantidade de entradas a serem geradas.</param>
        List<User> SeedUsers(int rowCount);
    }
}