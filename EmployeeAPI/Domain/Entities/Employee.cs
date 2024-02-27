using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string? EmployeeNum { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EmployeeDate { get; set; } = DateTime.Now;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime TerminatedDate { get; set; }

        public virtual Person? Person { get; set; }
    }
}
