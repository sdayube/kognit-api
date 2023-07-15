using Kognit.API.Domain.Entities;
using System.Collections.Generic;

namespace Kognit.API.Application.Interfaces
{
    public interface IMockService
    {
        List<User> GetPositions(int rowCount);

        List<Employee> GetEmployees(int rowCount);

        List<User> SeedPositions(int rowCount);
    }
}