using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Exceptions;
using PIMTool.Core.Domain.Objects.Employee;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;
using System.Linq.Dynamic.Core;
using PIMTool.Core.Exceptions.Employee;

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

            return employee ?? throw new EmployeeNotFoundException($"Employee {id} not found", id, visa: null);
        }

        public async Task<Boolean> AddAsync(AddEmployee employee, CancellationToken cancellationToken)
        {
            if ((DateTime.Now.Year - employee.Birthday.Year) < 17)
            {
                throw new BirthDayException($"Birthday : {employee.Birthday} is not valid", employee.Birthday);
            }

            var checkDuplicate = _repository
                .Get()
                .Where(e => employee.Visa.Contains(e.Visa))
                .FirstOrDefault();
            if (checkDuplicate != null)
            {
                throw new EmployeeDuplicateVisaException($"Visa {employee.Visa} is exsit", employee.Visa);
            }
            bool addCheck;
            try
            {
                var _employee = new Employee
                {
                    Visa = employee.Visa,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Birthday = employee.Birthday
                };

                await _repository.AddAsync(_employee, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);
                addCheck = true;
            }
            catch (Exception)
            {
                throw;
            }
            return addCheck;



        }

        public async Task<Employee?> UpdateAsync(UpdateEmployee updateEmployee, CancellationToken cancellationToken = default)
        {
            if ((DateTime.Now.Year - updateEmployee.Birthday.Year) < 17)
            {
                throw new BirthDayException($"Birthday : {updateEmployee.Birthday} is not valid", updateEmployee.Birthday);
            }

            var employee = await _repository.GetAsync(updateEmployee.Id, cancellationToken);
            if (employee == null)
            {
                throw new EmployeeNotFoundException(
                    $"Employee with id:{updateEmployee.Id} not found",
                    updateEmployee.Id,
                    updateEmployee.Visa);
            }
            //employee.Visa = updateEmployee.Visa;
            employee.FirstName = updateEmployee.FirstName;
            employee.LastName = updateEmployee.LastName;
            employee.Birthday = updateEmployee.Birthday;
            await _repository.SaveChangesAsync(cancellationToken);
            return employee;
        }

        public async Task<IEnumerable<Employee>?> GetAll(CancellationToken cancellationToken = default)
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
