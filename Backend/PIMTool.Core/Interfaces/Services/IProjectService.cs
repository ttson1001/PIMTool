using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Dtos.ProjectDtos.Request;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Project?> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<String> AddAsync(AddProjectDto addProject, CancellationToken cancellationToken = default);
        Task<List<Project>?> GetAll(CancellationToken cancellationToken = default);
        Task<String> UpdateAsync(UpdateProjectDto updateProject, CancellationToken cancellationToken = default);

        Task<List<Project>?> Search(SearchProjectDto searchProject, CancellationToken cancellationToken = default);

        Task<String> Delete(List<int> ids);
    }
}