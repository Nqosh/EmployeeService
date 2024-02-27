using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Domain.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace EmployeeAPI.Infrastructure.Data
{
    public class EmployeeRepository : IRepository
    {
        private readonly EmployeeDBContext _dbContext;
        private readonly ILogger _logger;

        public EmployeeRepository(EmployeeDBContext dBContext, ILogger logger)
        {
            _dbContext = dBContext;
            _logger = logger;
        }

        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            try
            {
                await _dbContext.Employees.AddAsync(employee);
                return await Save();
            }
            catch (Exception ex)
            {
                _logger.LogError($" Failed to create employee for {employee.EmployeeNum} with error {ex}");
            }
            return false;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                var employee = await GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return false;
                }

                _dbContext.Employees.Remove(employee);
                _dbContext.Persons.Remove(employee.Person);
                return await Save();
            }
            catch (Exception ex)
            {
                _logger.LogError($" Failed to delete employee for {id} with error {ex}");
            }
            return false;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _dbContext.Employees.Where(x => x.Id == id).Include("Person").FirstOrDefaultAsync();
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError($" Failed to get employee for {id} with error {ex}");
            }

            return null;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            try
            {
                var employees = await _dbContext.Employees.Include("Person").ToListAsync();
                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError($" Failed to get employees with error {ex}");
            }
            return null;
        }

        public async Task<bool> UpdateEmployeeAsync(int id, Employee employee)
        {
            try
            {
                var existingEmployee = await GetEmployeeByIdAsync(id);
                if (existingEmployee == null)
                {
                    return false;
                }

                _dbContext.Entry(existingEmployee).CurrentValues.SetValues(employee);
                return await Save();
            }
            catch (Exception ex)
            {
                _logger.LogError($" Failed to update employee for {id} with error {ex}");
            }

            return false;
        }

        public async Task<bool> Save()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool EmployeeExists(int id)
        {
            return _dbContext.Employees.Any(a => a.Id == id);
        }

        public bool EmployeeExists(string name)
        {
            bool value = _dbContext.Employees.Any(a => a.Person.FirstName.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

    }
}
