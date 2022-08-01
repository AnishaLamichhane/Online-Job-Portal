namespace WebApplication2.Models
{
    public class UpdateJobViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequiredSkills { get; set; }
        public DateTime PostedDate { get; set; }
        public long Salary { get; set; }
    }
}
