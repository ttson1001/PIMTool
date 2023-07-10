namespace PIMTool.Core.Dtos.ProjectDtos;

public class ProjectDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Customer { get; set; } = null!;
    public DateTime StartDate { get; set; }
}