using Kognit.API.Application.Exceptions;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<Guid>>
        {
            private readonly IUserRepositoryAsync _userRepository;

            public DeleteUserCommandHandler(IUserRepositoryAsync userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Response<Guid>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(command.Id) ?? throw new ApiException($"User not found.");
                await _userRepository.DeleteAsync(user);
                return new Response<Guid>(user.Id);
            }
        }
    }
}