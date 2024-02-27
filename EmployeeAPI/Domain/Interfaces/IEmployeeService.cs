using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Domain.Models;

namespace EmployeeAPI.Domain.Interfaces
{
    public interface IEmployeeService
    {
        bool EmployeeExists(string name);
        Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeByIdAsync(int id);
        Task<bool> AddEmployeeAsync(EmployeeCreateDTO employee);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<bool> UpdateEmployeeAsync(int id, Employee employee);
    }
}
