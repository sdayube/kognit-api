using AutoMapper;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Wrappers;
using Kognit.API.Domain.Entities;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Wallets.Commands.CreateWallet
{
    public partial class CreateWalletCommand : IRequest<Response<Wallet>>
    {
        public Guid UserId { get; set; }
        public string BankName { get; set; }
        public decimal Value { get; set; }
    }

    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, Response<Wallet>>
    {
        private readonly IWalletRepositoryAsync _walletRepository;
        private readonly IMapper _mapper;

        public CreateWalletCommandHandler(IWalletRepositoryAsync walletRepository, IMapper mapper)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
        }

        public async Task<Response<Wallet>> Handle(CreateWalletCommand request, System.Threading.CancellationToken cancellationToken)
        {
            var wallet = _mapper.Map<Wallet>(request);
            await _walletRepository.AddAsync(wallet);
            return new Response<Wallet>(wallet);
        }
    }
}
