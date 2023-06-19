using System.ComponentModel.DataAnnotations.Schema;

namespace SibersTest.Models.Request
{
    public class AddTask
    {
        public string Name { get; set; }
        public string Description { get; set; }
      
        
        public int ExecutorId { get; set; }
       
        public int ProjectId { get; set; }
        
        public StatusTask Status { get; set; }
     
    }
}
