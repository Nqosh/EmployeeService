using AutoMapper;
using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Domain.Interfaces;
using EmployeeAPI.Domain.Models;
using EmployeeAPI.Infrastructure.Data;
using System.Runtime.InteropServices;

namespace EmployeeAPI.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddEmployeeAsync(EmployeeCreateDTO employee)
        {
            var employeeObj = _mapper.Map<Employee>(employee);
            var personObj = _mapper.Map<Person>(employee);
            employeeObj.Person = personObj;
            if (await _employeeRepository.AddEmployeeAsync(employeeObj)) { return true; }
            return false;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            if(await _employeeRepository.DeleteEmployeeAsync(id)) { return true; }
            return false;
        }

        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            return employeeDTO;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync()
        {
            var objList = await _employeeRepository.GetEmployeesAsync();
            var employeeDto = new List<EmployeeDTO>();
            foreach (var obj in objList)
            {
                employeeDto.Add(_mapper.Map<EmployeeDTO>(obj));
            }
            return employeeDto;
        }

        public bool EmployeeExists(string name)
        {
            if(_employeeRepository.EmployeeExists(name)) return true;
            return false;
        }

        public async Task<bool> UpdateEmployeeAsync(int id, Employee employee)
        {
            var employeeObj = _mapper.Map<Employee>(employee);

            if (await _employeeRepository.UpdateEmployeeAsync(id, employeeObj)) return true;
            return false;
        }
    }
}
