using Kognit.API.Application.Exceptions;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Wrappers;
using Kognit.API.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<Response<User>>
    {
        public Guid Id { get; set; }

        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<User>>
        {
            private readonly IUserRepositoryAsync _userRepository;

            public GetUserByIdQueryHandler(IUserRepositoryAsync userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Response<User>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(query.Id);
                if (user == null) throw new ApiException("User not found.");
                return new Response<User>(user);
            }
        }
    }
}