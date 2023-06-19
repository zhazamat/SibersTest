namespace SibersTest.Models.Response
{
    public class GetTaskResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        public string  Executor { get; set; }

        public string Project { get; set; }

        public string Status { get; set; }
       
    }
}
