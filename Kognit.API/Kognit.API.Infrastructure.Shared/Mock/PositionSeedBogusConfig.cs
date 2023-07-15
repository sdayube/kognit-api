using AutoBogus;
using Bogus;
using Bogus.Extensions.Brazil;
using Kognit.API.Domain.Entities;
using System;

namespace Kognit.API.Infrastructure.Shared.Mock
{
    public class PositionSeedBogusConfig : AutoFaker<User>
    {
        public PositionSeedBogusConfig()
        {
            Randomizer.Seed = new Random(8675309);
            RuleFor(m => m.Id, f => Guid.NewGuid());
            RuleFor(o => o.Name, f => f.Name.FullName());
            RuleFor(o => o.BirthDate, f => f.Date.Past(100, DateTime.Now.AddYears(-18)));
            RuleFor(o => o.CPF, f => f.Person.Cpf(false));
            RuleFor(o => o.Created, f => f.Date.Past(1));
            RuleFor(o => o.LastModified, f => f.Date.Recent(1));
        }
    }
}