using AutoMapper;
using Kognit.API.Application.Interfaces;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Application.Parameters;
using Kognit.API.Application.Wrappers;
using Kognit.API.Domain.Common;
using Kognit.API.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kognit.API.Application.Features.Employees.Queries.GetEmployees
{
    /// <summary>
    /// GetAllEmployeesQuery - handles media IRequest
    /// BaseRequestParameter - contains paging parameters
    /// To add filter/search parameters, add search properties to the body of this class
    /// </summary>
    public class GetEmployeesQuery : QueryParameter, IRequest<PaginatedResponse<IEnumerable<DynamicEntity<Employee>>>>
    {
        //examples:
        public string EmployeeNumber { get; set; }
        public string EmployeeTitle { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

    }

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, PaginatedResponse<IEnumerable<DynamicEntity<Employee>>>>
    {
        private readonly IEmployeeRepositoryAsync _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;



        /// <summary>
        /// Constructor for GetAllEmployeesQueryHandler class.
        /// </summary>
        /// <param name="employeeRepository">IEmployeeRepositoryAsync object.</param>
        /// <param name="mapper">IMapper object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        /// <returns>
        /// GetAllEmployeesQueryHandler object.
        /// </returns>
        public GetAllEmployeesQueryHandler(IEmployeeRepositoryAsync employeeRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }



        /// <summary>
        /// Handles the GetEmployeesQuery request and returns a PaginatedResponse containing the requested data.
        /// </summary>
        /// <param name="request">The GetEmployeesQuery request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A PaginatedResponse containing the requested data.</returns>
        public async Task<PaginatedResponse<IEnumerable<DynamicEntity<Employee>>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetEmployeesViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetEmployeesViewModel>();
            }
            // query based on filter
            var entityEmployees = await _employeeRepository.GetPagedEmployeeResponseAsync(validFilter);
            var data = entityEmployees.data;
            RecordsCount recordCount = entityEmployees.recordsCount;

            // response wrapper
            return new PaginatedResponse<IEnumerable<DynamicEntity<Employee>>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}