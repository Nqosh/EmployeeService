using AutoMapper;
using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Domain.Models;

namespace EmployeeAPI.Application.Mappings
{
    public class EmployeeManagementSystemMapping : Profile
    {
        public EmployeeManagementSystemMapping()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();
            CreateMap<Person, EmployeeCreateDTO>().ReverseMap();
        }
    }
}
