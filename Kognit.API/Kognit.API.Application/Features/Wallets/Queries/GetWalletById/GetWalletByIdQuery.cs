using Kognit.API.Application.Exceptions;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Wrappers;
using Kognit.API.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Wallets.Queries.GetWalletById
{
    public class GetWalletByIdQuery : IRequest<Response<Wallet>>
    {
        public Guid Id { get; set; }

        public class GetWalletByIdQueryHandler : IRequestHandler<GetWalletByIdQuery, Response<Wallet>>
        {
            private readonly IWalletRepositoryAsync _walletRepository;

            public GetWalletByIdQueryHandler(IWalletRepositoryAsync walletRepository)
            {
                _walletRepository = walletRepository;
            }

            public async Task<Response<Wallet>> Handle(GetWalletByIdQuery query, CancellationToken cancellationToken)
            {
                var wallet = await _walletRepository.GetByIdAsync(query.Id);
                if (wallet == null) throw new ApiException("Wallet not found.");
                return new Response<Wallet>(wallet);
            }
        }
    }
}