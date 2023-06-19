using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


using SibersTest.Models;

namespace SibersTest.Models
{
    [Table("employees")]
    public class Employee
    {
        public Employee()
        {
          
            EmployeeProjects = new HashSet<EmployeeProject>();
            Tasks = new HashSet<Task>();
        }
      
        [Key,Required]
        public int Id { get; set; }
      
        public string FullName { get; set; } = string.Empty;
      
        public string PositionTitle { get; set; } = string.Empty;
       
        public string Mobile { get; set; } = string.Empty;
       
        public string Email { get; set; } = string.Empty;
        [NotMapped]
        public string linkImg { get; set; } = string.Empty;
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }

        public ICollection<Task> Tasks { get; set; }
        
    }
}
