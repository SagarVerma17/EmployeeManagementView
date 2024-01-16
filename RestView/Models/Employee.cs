using System.ComponentModel.DataAnnotations;

namespace RestView.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Name should contain only alphabetic characters")]
        public required string EmpName { get; set; }

        [Required]
        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60")]
        public int EmpAge { get; set; }

        public DateTime JoiningDate { get; set; }

        public required String EmpAddress { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number should be numeric and 10 digits")]
        public required long EmpMobileNo { get; set; }

        public required string EmpDeptNo { get; set; }

        public Employee()
        {
            
        }
    }
    
}
