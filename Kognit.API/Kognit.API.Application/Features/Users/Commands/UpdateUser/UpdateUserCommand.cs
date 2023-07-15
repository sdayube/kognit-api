using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string CPF { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response<Guid>>
        {
            private readonly IUserRepositoryAsync _userRepository;

            public UpdateUserCommandHandler(IUserRepositoryAsync userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Response<Guid>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(command.Id);

                if (user == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }
                else
                {
                    user.Name = command.Name;
                    user.BirthDate = command.BirthDate;
                    user.CPF = command.CPF;

                    await _userRepository.UpdateAsync(user);

                    return new Response<Guid>(user.Id);
                }
            }
        }
    }
}