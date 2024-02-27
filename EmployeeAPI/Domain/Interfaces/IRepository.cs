using EmployeeAPI.Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace EmployeeAPI.Domain.Interfaces
{
    public interface IRepository
    {
        Task<bool> Save();
        bool EmployeeExists(int id);
        bool EmployeeExists(string name);

        Task<IEnumerable<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int id);

        Task<bool> AddEmployeeAsync(Employee employee);

        Task<bool> DeleteEmployeeAsync(int id);

        Task<bool> UpdateEmployeeAsync(int id, Employee employee);
    }
}
