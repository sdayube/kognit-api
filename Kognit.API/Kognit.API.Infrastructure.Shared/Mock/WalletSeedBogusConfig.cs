using AutoBogus;
using Bogus;
using Kognit.API.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Kognit.API.Infrastructure.Shared.Mock
{
    public class WalletSeedBogusConfig : AutoFaker<Wallet>
    {
        public WalletSeedBogusConfig(IEnumerable<User> users)
        {
            Randomizer.Seed = new Random(8675309);

            RuleFor(p => p.Id, f => Guid.NewGuid());
            RuleFor(p => p.UserId, f => f.PickRandom(users).Id);
            RuleFor(p => p.Value, f => f.Random.Decimal(0, 100000));
            RuleFor(p => p.BankName, f => f.Company.CompanyName());

            Ignore(p => p.User);
        }
    }
}