using AutoMapper;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Wrappers;
using Kognit.API.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Users.Commands.CreateUser
{
    public partial class CreateUserCommand : IRequest<Response<User>>
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string CPF { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<User>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            await _userRepository.AddAsync(user);
            return new Response<User>(user);
        }
    }
}