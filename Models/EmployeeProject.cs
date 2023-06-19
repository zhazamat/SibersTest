
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SibersTest.Models
{
    [Table("employee_projects")]
    public class EmployeeProject
    {
        public EmployeeProject()
        {
            Tasks = new HashSet<Task>();
        }
        [Key, Required]
        public int Id { get; set; }


        public int? EmployeeId { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; }

       public int? ProjectId { get; set; }
        [JsonIgnore]
        public virtual Project Project{get;set;}
        public ICollection<Task> Tasks { get; set; }

    }
}
