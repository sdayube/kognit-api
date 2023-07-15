using Kognit.API.Domain.Entities;
using System.Collections.Generic;

namespace Kognit.API.Application.Interfaces
{
    public interface IMockService
    {
        List<Position> GetPositions(int rowCount);

        List<Employee> GetEmployees(int rowCount);

        List<Position> SeedPositions(int rowCount);
    }
}