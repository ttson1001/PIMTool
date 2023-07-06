using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects.ProjectEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IProjectEmployeeService
    {
        Task<String> AddAsync(AddProjectEmployee addProjectEmployee, CancellationToken cancellationToken = default);


    }
}
