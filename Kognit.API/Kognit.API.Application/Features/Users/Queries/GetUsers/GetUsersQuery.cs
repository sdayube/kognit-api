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

namespace Kognit.API.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQuery : QueryParameter, IRequest<PaginatedResponse<IEnumerable<DynamicEntity<User>>>>
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedResponse<IEnumerable<DynamicEntity<User>>>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IModelHelper _modelHelper;

        public GetAllUsersQueryHandler(IUserRepositoryAsync userRepository, IModelHelper modelHelper)
        {
            _userRepository = userRepository;
            _modelHelper = modelHelper;
        }

        public async Task<PaginatedResponse<IEnumerable<DynamicEntity<User>>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {

            var validFilter = request;

            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                validFilter.Fields = _modelHelper.ValidateModelFields<GetUsersViewModel>(validFilter.Fields);
            }
            else
            {
                validFilter.Fields = _modelHelper.GetModelFields<GetUsersViewModel>();
            }

            var entityUsers = await _userRepository.GetPaginatedUserReponseAsync(validFilter);
            var data = entityUsers.data;
            RecordsCount recordCount = entityUsers.recordsCount;

            return new PaginatedResponse<IEnumerable<DynamicEntity<User>>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}