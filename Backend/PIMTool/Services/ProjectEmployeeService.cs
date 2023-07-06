using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;

namespace PIMTool.Services
{
    public class ProjectEmployeeService : IProjectEmployeeService
    {
        private readonly IRepository<ProjectEmployee> _repository;

        public ProjectEmployeeService(IRepository<ProjectEmployee> repository)
        {
            _repository = repository;
        }

        public Task<string> AddAsync(AddProjectEmployee addProjectEmployee, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
