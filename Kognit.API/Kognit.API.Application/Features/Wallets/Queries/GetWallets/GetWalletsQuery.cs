using Kognit.API.Application.Interfaces;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Parameters;
using Kognit.API.Application.Wrappers;
using Kognit.API.Domain.Common;
using Kognit.API.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Wallets.Queries.GetWallets
{
    public class GetWalletsQuery : QueryParameter, IRequest<PaginatedResponse<IEnumerable<DynamicEntity<Wallet>>>>
    {
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public string BankName { get; set; }
    }

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetWalletsQuery, PaginatedResponse<IEnumerable<DynamicEntity<Wallet>>>>
    {
        private readonly IWalletRepositoryAsync _walletRepository;
        private readonly IModelHelper _modelHelper;

        public GetAllEmployeesQueryHandler(IWalletRepositoryAsync walletRepository, IModelHelper modelHelper)
        {
            _walletRepository = walletRepository;
            _modelHelper = modelHelper;
        }

        public async Task<PaginatedResponse<IEnumerable<DynamicEntity<Wallet>>>> Handle(GetWalletsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;

            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                validFilter.Fields = _modelHelper.ValidateModelFields<GetWalletsViewModel>(validFilter.Fields);
            }
            else
            {
                validFilter.Fields = _modelHelper.GetModelFields<GetWalletsViewModel>();
            }

            var entityWallets = await _walletRepository.GetPaginatedWalletResponseAsync(validFilter);
            var data = entityWallets.data;
            RecordsCount recordCount = entityWallets.recordsCount;

            return new PaginatedResponse<IEnumerable<DynamicEntity<Wallet>>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}