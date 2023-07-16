using Kognit.API.Application.Features.Users.Commands.UpdateUser;
using System;

namespace Kognit.API.WebApi.Requests
{
    public class UpdateUserRequest
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string CPF { get; set; }

        public UpdateUserCommand ToCommand(Guid id)
        {
            return new UpdateUserCommand
            {
                Id = id,
                Name = Name,
                BirthDate = BirthDate,
                CPF = CPF
            };
        }
    }
}
