using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Dtos.ProjectDtos.Request;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Project?> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<Boolean> AddAsync(RequestProjectDto projectAddDto, CancellationToken cancellationToken = default);
        Task<List<Project>?> GetAll(CancellationToken cancellationToken = default);
        Task<Project?> UpdateAsync(RequestProjectDto projectUpdate, CancellationToken cancellationToken = default);
        Task<List<Project>?> Search(ProjectSearchDto searchProject, CancellationToken cancellationToken = default);
        Task<Boolean> Delete(List<int> ids);
        Boolean CheckProjectNumber(int projectNumber);
    }
}