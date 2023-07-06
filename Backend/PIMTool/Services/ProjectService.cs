using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;

namespace PIMTool.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _repository;

        public ProjectService(IRepository<Project> repository)
        {
            _repository = repository;
        }

        public async Task<Project?> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(id, cancellationToken);
            return entity;
        }
        public async Task<Project?> AddAsync(Project project, CancellationToken cancellationToken = default)
        {
            Project project1 = new Project
            {
                Name = project.Name,
            };
            await _repository.AddAsync(project1, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return project;
        }
    }
}