using Kognit.API.Domain.Entities;
using System;

namespace Kognit.API.Application.Features.Wallets.Queries.GetWallets
{
    public class GetWalletsViewModel
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public decimal Value { get; set; }
        public string BankName { get; set; }
    }
}