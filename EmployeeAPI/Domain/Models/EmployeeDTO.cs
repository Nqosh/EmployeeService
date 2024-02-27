using EmployeeAPI.Domain.Entities;

namespace EmployeeAPI.Domain.Models
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string? EmployeeNum { get; set; }
        public DateTime EmployeeDate { get; set; }
        public DateTime TerminatedDate { get; set;}
        public Person? Person { get; set; }
    }
}
