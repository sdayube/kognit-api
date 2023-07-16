using Kognit.API.Application.Features.Wallets.Commands.UpdateWallet;
using System;

namespace Kognit.API.WebApi.Requests
{
    public class UpdateWalletRequest
    {
        public Guid UserId { get; set; }
        public string BankName { get; set; }
        public decimal Value { get; set; }

        public UpdateWalletCommand ToCommand(Guid id)
        {
            return new UpdateWalletCommand
            {
                Id = id,
                UserId = UserId,
                BankName = BankName,
                Value = Value
            };
        }
    }
}
