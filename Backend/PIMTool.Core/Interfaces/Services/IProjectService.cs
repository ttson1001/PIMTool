using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Project?> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<Project?> AddAsync(Project project, CancellationToken cancellationToken = default);


    }
}