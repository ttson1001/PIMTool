using PIMTool.Core.Domain.Objects;


namespace PIMTool.Core.Interfaces.Services
{
    public interface IProjectEmployeeService
    {
        Task<String> AddAsync(AddProjectEmployee addProjectEmployee, CancellationToken cancellationToken = default);


    }
}
