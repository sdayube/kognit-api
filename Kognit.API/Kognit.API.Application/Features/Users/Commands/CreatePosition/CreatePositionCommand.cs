using AutoMapper;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Wrappers;
using Kognit.API.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Users.Commands.CreatePosition
{
    public partial class CreatePositionCommand : IRequest<Response<Guid>>
    {
        public string PositionTitle { get; set; }
        public string PositionNumber { get; set; }
        public string PositionDescription { get; set; }
        public decimal PositionSalary { get; set; }
    }

    public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, Response<Guid>>
    {
        private readonly IUserRepositoryAsync _positionRepository;
        private readonly IMapper _mapper;

        public CreatePositionCommandHandler(IUserRepositoryAsync positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            var position = _mapper.Map<User>(request);
            await _positionRepository.AddAsync(position);
            return new Response<Guid>(position.Id);
        }
    }
}