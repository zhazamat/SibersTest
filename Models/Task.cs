using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SibersTest.Models
{
    [Table("tasks")]
    public class Task
    {
        [Key, Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AuthorId { get; set; }
        [JsonIgnore]
        public virtual Employee Author { get; set; }
        public int? ExecutorId { get; set; }
        [JsonIgnore]
        public virtual EmployeeProject Executor { get; set; }
        public int?  ProjectId {get;set;}
       [JsonIgnore]
        public virtual Project Project { get; set; }
       
        public StatusTask Status { get; set; }

    }
    public enum StatusTask{
        ToDo,
        InProgress,
        Done
    }
}
