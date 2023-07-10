using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects.Group;
using PIMTool.Core.Exceptions.Employee;
using PIMTool.Core.Exceptions.Group;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;
using System.Security.Cryptography;

namespace PIMTool.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRepository<Group> _groupRepository;

        private readonly IRepository<Employee> _employeeRepository;

        public GroupService(IRepository<Group> groupRepository, IRepository<Employee> employeeRepository)
        {
            _groupRepository = groupRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Boolean> AddAsync(AddGroup addGroup, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository
                .GetAsync(addGroup.EmployeeId, cancellationToken);
            if (employee == null)
            {
                throw new EmployeeNotFoundException($"Employee {addGroup.EmployeeId} not found", addGroup.EmployeeId);
            }
            bool addCheck;
            try
            {
                var newGroup = new Group
                {
                    Employee = employee
                };

                await _groupRepository.AddAsync(newGroup, cancellationToken);
                await _groupRepository.SaveChangesAsync(cancellationToken);
                addCheck = true;
            }
            catch (Exception)
            {
                throw;
            }
            return addCheck;

        }

        public async Task<List<Group>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entity = await _groupRepository.Get().Include(x => x.Employee).ToListAsync(cancellationToken);
            return entity;
        }

        public async Task<Group?> GetAsync(int id, CancellationToken cancellationToken = default)
        {

            var entity = await _groupRepository
                .Get()
                .Where(x => x.Id == id)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(cancellationToken);

            return entity ?? throw new GroupNotFoundException($"Group with id: {id} not found", id);
        }

        public async Task<Group?> UpdateAsync(UpdateGroup updateGroup, CancellationToken cancellationToken = default)
        {
            // Find employee and check not null
            var employee = await _employeeRepository.GetAsync(updateGroup.GroupLeaderId, cancellationToken);
            if (employee == null)
            {
                throw new EmployeeNotFoundException(
                    $"Employee {updateGroup.GroupLeaderId} not found",
                    updateGroup.GroupLeaderId);
            }
            // Find group and check not null
            var changeGroup = await _groupRepository.GetAsync(updateGroup.Id, cancellationToken);
            if(changeGroup == null)
            {
                throw new GroupNotFoundException(
                    $"Group with id: {updateGroup.Id} not found",
                    updateGroup.Id);
            }
            // update Group
            changeGroup.Employee = employee;
            await _employeeRepository.SaveChangesAsync(cancellationToken);

            return changeGroup;
        }
    }
}
