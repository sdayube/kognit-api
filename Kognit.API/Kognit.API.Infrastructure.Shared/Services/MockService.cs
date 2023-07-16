using Kognit.API.Application.Interfaces;
using Kognit.API.Domain.Entities;
using Kognit.API.Infrastructure.Shared.Mock;
using System.Collections.Generic;

namespace Kognit.API.Infrastructure.Shared.Services
{
    public class MockService : IMockService
    {
        public List<Wallet> SeedWallets(int rowCount, IEnumerable<User> users)
        {
            var faker = new WalletSeedBogusConfig(users);
            return faker.Generate(rowCount);
        }

        public List<User> SeedUsers(int rowCount)
        {
            var faker = new UserSeedBogusConfig();
            return faker.Generate(rowCount);
        }
    }
}