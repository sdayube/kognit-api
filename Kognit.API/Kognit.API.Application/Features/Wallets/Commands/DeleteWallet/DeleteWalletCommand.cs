using Kognit.API.Application.Exceptions;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Wallets.Commands.DeleteWallet
{
    public class DeleteWalletCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public class DeleteWalletCommandHandler : IRequestHandler<DeleteWalletCommand, Response<Guid>>
        {
            private readonly IWalletRepositoryAsync _walletRepository;

            public DeleteWalletCommandHandler(IWalletRepositoryAsync walletRepository)
            {
                _walletRepository = walletRepository;
            }

            public async Task<Response<Guid>> Handle(DeleteWalletCommand command, CancellationToken cancellationToken)
            {
                var wallet = await _walletRepository.GetByIdAsync(command.Id) ?? throw new ApiException($"Wallet not found.");
                await _walletRepository.DeleteAsync(wallet);
                return new Response<Guid>(wallet.Id);
            }
        }
    }
}