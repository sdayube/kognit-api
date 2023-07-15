using System;

namespace Kognit.API.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string CPF { get; set; }
    }
}