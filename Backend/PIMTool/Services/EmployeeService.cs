using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects.Employee;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;
using System.Linq.Dynamic.Core;

namespace PIMTool.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;

        public EmployeeService(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task<Employee?> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var employee = await _repository.GetAsync(id, cancellationToken);
            return employee;
        }

        public async Task<String> AddAsync(AddEmployee employee, CancellationToken cancellationToken)
        {
            //using (UnitOfWork unit = new())
            //{
            //    var b = unit.EmployeeRepository.GetAsync(1, cancellationToken);
            //    await unit.SaveChangesAsync();
            //}
                var _employee = new Employee
                {
                    Visa = employee.Visa,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Birthday = employee.Birthday
                };

            await _repository.AddAsync(_employee, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return "Add Successfull!!";


        }

        public async Task<Employee?> UpdateAsync(UpdateEmployee updateEmployee, CancellationToken cancellationToken = default)
        {

            var employee = await _repository.GetAsync(updateEmployee.Id, cancellationToken);
            employee.Visa = updateEmployee.Visa;
            employee.FirstName = updateEmployee.FirstName;
            employee.LastName = updateEmployee.LastName;
            employee.Birthday = updateEmployee.Birthday;
            await _repository.SaveChangesAsync(cancellationToken);
            return employee;
        }

        public async Task<List<Employee>?> GetAll(CancellationToken cancellationToken = default)
        {
            var employees = await _repository.GetValuesAsync(cancellationToken);
            return employees;
        }

        public string Delete(List<int> listId)
        {
            List<Employee> Employees = new();
            listId.ForEach(x =>
            {
                Employees.Add(_repository.Get().Where(i => i.Id == x).First());
            });

            _repository.Delete(Employees);
            _repository.SaveChangesAsync();

            return "Delete successfull";
        }

       
    }
}
