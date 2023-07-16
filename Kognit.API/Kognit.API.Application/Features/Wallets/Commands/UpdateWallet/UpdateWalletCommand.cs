using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Wallets.Commands.UpdateWallet
{
    public class UpdateWalletCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string BankName { get; set; }
        public decimal Value { get; set; }
    }

    public class UpdateWalletCommandHandler : IRequestHandler<UpdateWalletCommand, Response<Guid>>
    {
        private readonly IWalletRepositoryAsync _walletRepository;

        public UpdateWalletCommandHandler(IWalletRepositoryAsync walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<Response<Guid>> Handle(UpdateWalletCommand request, System.Threading.CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetByIdAsync(request.Id);

            if (wallet == null)
            {
                throw new KeyNotFoundException("Wallet not found.");
            }
            else
            {
                wallet.UserId = request.UserId;
                wallet.BankName = request.BankName;
                wallet.Value = request.Value;

                await _walletRepository.UpdateAsync(wallet);

                return new Response<Guid>(wallet.Id);
            }
        }
    }
}
