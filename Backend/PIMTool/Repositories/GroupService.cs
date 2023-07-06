using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects.Group;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;

namespace PIMTool.Repositories
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

        public async Task<string> AddAsync(AddGroup addGroup, CancellationToken cancellationToken = default)
        {
            var entity = await _employeeRepository
                .GetAsync(addGroup.EmployeeId, cancellationToken);
            var newGroup = new Group
            {
                Employee = entity
            };
            
            await _groupRepository.AddAsync(newGroup, cancellationToken);
            await _groupRepository.SaveChangesAsync(cancellationToken);
            return "Add Susscessfull!!!";
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

            return entity;
        }

        public async Task<Group?> UpdateAsync(UpdateGroup updateGroup, CancellationToken cancellationToken = default)
        {
            var changeGroup = await _groupRepository.GetAsync(updateGroup.Id, cancellationToken);
            changeGroup.Employee = await _employeeRepository.GetAsync(updateGroup.GroupLeaderId, cancellationToken);
            await _employeeRepository.SaveChangesAsync(cancellationToken);

            return changeGroup;
        }
    }
}
