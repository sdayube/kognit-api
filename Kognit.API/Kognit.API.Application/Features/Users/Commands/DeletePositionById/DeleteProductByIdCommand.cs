using Kognit.API.Application.Exceptions;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Users.Commands.DeletePositionById
{
    public class DeletePositionByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public class DeletePositionByIdCommandHandler : IRequestHandler<DeletePositionByIdCommand, Response<Guid>>
        {
            private readonly IUserRepositoryAsync _positionRepository;

            public DeletePositionByIdCommandHandler(IUserRepositoryAsync positionRepository)
            {
                _positionRepository = positionRepository;
            }

            public async Task<Response<Guid>> Handle(DeletePositionByIdCommand command, CancellationToken cancellationToken)
            {
                var position = await _positionRepository.GetByIdAsync(command.Id);
                if (position == null) throw new ApiException($"Position Not Found.");
                await _positionRepository.DeleteAsync(position);
                return new Response<Guid>(position.Id);
            }
        }
    }
}