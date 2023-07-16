using AutoMapper;
using Kognit.API.Application.Features.Employees.Queries.GetEmployees;
using Kognit.API.Application.Features.Users.Commands.CreateUser;
using Kognit.API.Application.Features.Users.Queries.GetUsers;
using Kognit.API.Domain.Entities;

namespace Kognit.API.Application.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, GetUsersViewModel>().ReverseMap();
            CreateMap<Employee, GetEmployeesViewModel>().ReverseMap();
            CreateMap<CreateUserCommand, User>();
        }
    }
}