using System.ComponentModel.DataAnnotations;

namespace PIMTool.Core.Domain.Entities
{
    public class Project : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int ProjectNumber { get; set; }

        public string Customer { get; set; }

        public string Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; } 

        public int GroupId { get; set; }

        public Group? Group { get; set; } = null;
        public byte[]? Version { get; set; }

        public ICollection<ProjectEmployee> projectEmployees { get; set; } = null!;

    }
}