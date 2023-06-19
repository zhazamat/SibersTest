namespace SibersTest.Models.Request
{
    public class UpdateTask
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }

        public int ExecutorId { get; set; }

        public int ProjectId { get; set; }

        public StatusTask Status { get; set; }
    }
}
