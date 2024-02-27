namespace EmployeeAPI.Domain.Models
{
    public class EmployeeCreateDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmployeeNum { get; set; }
        public DateTime BirthDate { get; set; }

    }
}
