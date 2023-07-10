namespace PIMTool.Core.Dtos.ProjectDtos.Request
{
    public class AddProjectDto
    {
        public string Name { get; set; } = null!;

        public int ProjectNumber { get; set; }

        public string Customer { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int GroupId { get; set; }

        public String Members { get; set; }= null!;
    }
}
