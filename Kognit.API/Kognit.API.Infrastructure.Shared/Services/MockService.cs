using Kognit.API.Application.Interfaces;
using Kognit.API.Domain.Entities;
using Kognit.API.Infrastructure.Shared.Mock;
using System.Collections.Generic;

namespace Kognit.API.Infrastructure.Shared.Services
{
    public class MockService : IMockService
    {


        /// <summary>
        /// Generates a list of positions using the UserInsertBogusConfig class.
        /// </summary>
        /// <param name="rowCount">The number of positions to generate.</param>
        /// <returns>A list of generated positions.</returns>
        public List<User> GetPositions(int rowCount)
        {
            var faker = new UserInsertBogusConfig();
            return faker.Generate(rowCount);
        }



        /// <summary>
        /// Gets a list of Employees using the EmployeeBogusConfig class.
        /// </summary>
        /// <param name="rowCount">The number of Employees to generate.</param>
        /// <returns>A list of Employees.</returns>
        public List<Employee> GetEmployees(int rowCount)
        {
            var faker = new EmployeeBogusConfig();
            return faker.Generate(rowCount);
        }

        public List<User> SeedUsers(int rowCount)
        {
            var faker = new UserSeedBogusConfig();
            return faker.Generate(rowCount);
        }
    }
}