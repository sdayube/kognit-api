using Bogus;
using Bogus.Extensions.Brazil;
using Kognit.API.Domain.Entities;
using System;


namespace Kognit.API.Infrastructure.Shared.Mock
{
    public class PositionInsertBogusConfig : Faker<User>
    {
        public PositionInsertBogusConfig()
        {
            RuleFor(o => o.Name, f => f.Name.FullName());
            RuleFor(o => o.BirthDate, f => f.Date.Past(100, DateTime.Now.AddYears(-18)));
            RuleFor(o => o.CPF, f => f.Person.Cpf(false));
        }
    }
}
