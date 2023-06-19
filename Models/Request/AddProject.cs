namespace SibersTest.Models.Request
{
    public class AddProject
    {
        public string ProjectName { get; set; }
        public string ClientCompany { get; set; }
        public string ExecutingCompany { get; set; }
        public int ProjectManagerId { get; set; }
        public int Priority { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
