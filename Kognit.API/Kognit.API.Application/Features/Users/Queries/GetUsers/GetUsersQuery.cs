using AutoMapper;
using Kognit.API.Application.Interfaces;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Parameters;
using Kognit.API.Application.Wrappers;
using Kognit.API.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<DynamicEntity>>>
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedResponse<IEnumerable<DynamicEntity>>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetAllUsersQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }

        public async Task<PagedResponse<IEnumerable<DynamicEntity>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {

            var validFilter = request;

            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                validFilter.Fields = _modelHelper.ValidateModelFields<GetUsersViewModel>(validFilter.Fields);
            }

            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                validFilter.Fields = _modelHelper.GetModelFields<GetUsersViewModel>();
            }

            var entityUsers = await _userRepository.GetPaginatedUserReponseAsync(validFilter);
            var data = entityUsers.data;
            RecordsCount recordCount = entityUsers.recordsCount;

            return new PagedResponse<IEnumerable<DynamicEntity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}