using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SibersTest.Models
{
    [Table("projects")]
    public class Project
    {
        public Project()
        {

            EmployeeProjects = new HashSet<EmployeeProject>();
            Tasks = new HashSet<Task>();
        }
        [Key, Required]
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ClientCompany { get; set; }
        public string ExecutingCompany { get; set; }
        public int? ProjectManagerId { get; set; }
        [JsonIgnore]
        public virtual Employee ProjectManager { get; set; }
        public int Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
