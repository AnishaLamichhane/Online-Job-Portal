namespace WebApplication2.Models.Domain
{
    public class Job
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequiredSkills { get; set; }
        public DateTime PostedDate { get; set; }
        public long Salary { get; set; }

    }
}
