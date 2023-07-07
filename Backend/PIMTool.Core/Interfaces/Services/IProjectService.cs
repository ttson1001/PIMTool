using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects.Project;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Project?> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<String> AddAsync(AddProject addProject, CancellationToken cancellationToken = default);
        Task<List<Project>?> GetAll(CancellationToken cancellationToken = default);
        Task<String> UpdateAsync(UpdateProject updateProject, CancellationToken cancellationToken = default);

        Task<List<Project>?> Search(SearchProject searchProject, CancellationToken cancellationToken = default);

        Task<String> Delete(List<int> ids);
    }
}