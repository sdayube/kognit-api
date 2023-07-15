using AutoMapper;
using Kognit.API.Application.Features.Employees.Queries.GetEmployees;
using Kognit.API.Application.Features.Positions.Commands.CreatePosition;
using Kognit.API.Application.Features.Positions.Queries.GetPositions;
using Kognit.API.Domain.Entities;

namespace Kognit.API.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Position, GetPositionsViewModel>().ReverseMap();
            CreateMap<Employee, GetEmployeesViewModel>().ReverseMap();
            CreateMap<CreatePositionCommand, Position>();
        }
    }
}