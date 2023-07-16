using AutoMapper;
using Kognit.API.Application.Features.Users.Commands.CreateUser;
using Kognit.API.Application.Features.Users.Queries.GetUsers;
using Kognit.API.Application.Features.Wallets.Commands.CreateWallet;
using Kognit.API.Application.Features.Wallets.Queries.GetWallets;
using Kognit.API.Domain.Entities;

namespace Kognit.API.Application.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, GetUsersViewModel>().ReverseMap();
            CreateMap<CreateUserCommand, User>();

            CreateMap<Wallet, GetWalletsViewModel>().ReverseMap();
            CreateMap<CreateWalletCommand, Wallet>();
        }
    }
}