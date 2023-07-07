using PIMTool.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIMTool.Core.Domain.Objects.Employee;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<Boolean> AddAsync(AddEmployee employee, CancellationToken cancellationToken = default);

        Task<Employee?> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<Employee?> UpdateAsync(UpdateEmployee updateEmployee, CancellationToken cancellationToken = default);

        Task<IEnumerable<Employee>?> GetAll(CancellationToken cancellationToken= default);

        String Delete(List<int> listId);
    }
}
