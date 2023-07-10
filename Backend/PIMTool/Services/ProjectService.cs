using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Dtos.ProjectDtos.Request;
using PIMTool.Core.Exceptions.Employee;
using PIMTool.Core.Exceptions.Group;
using PIMTool.Core.Exceptions.Project;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;
using System.Threading;

namespace PIMTool.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _repository;
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<ProjectEmployee> _projectEmployeeRepository;
        private readonly IMapper _mapper;

        public ProjectService(
            IRepository<Project> repository,
            IRepository<Group> groupRepository,
            IRepository<Employee> employeeRepository,
            IRepository<ProjectEmployee> projectEmployeeRepository,
            IMapper mapper)

        {
            _mapper = mapper;
            _repository = repository;
            _groupRepository = groupRepository;
            _employeeRepository = employeeRepository;
            _projectEmployeeRepository = projectEmployeeRepository;
        }

        public async Task<Project?> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.Get()
                .Where(x => x.Id == id)
                .Include(x => x.Group)
                .Include(x => x.projectEmployees)
                .ThenInclude(x => x.Employee)
                .FirstOrDefaultAsync(cancellationToken);

            if(entity == null)
            {
                throw new ProjectNotFoundException($"Project with id: {id} not found ", id);
            }

            return entity;
        }

        public async Task<string> AddAsync(AddProjectDto addProject, CancellationToken cancellationToken = default)
        {
            var group = await _groupRepository.GetAsync(addProject.GroupId, cancellationToken);
            if(group == null)
            {
                throw new GroupNotFoundException($"Group with id: {addProject.GroupId} not found", addProject.GroupId);
            }

            Project project = new()
            {
                Name = addProject.Name,
                ProjectNumber = addProject.ProjectNumber,
                Customer = addProject.Customer,
                Group = group,
                Status = addProject.Status,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            };
            await _repository.AddAsync(project, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            SaveProjectEmployee(addProject.Members,project,cancellationToken);
            return "Add successfull";

        }

        public async Task<string> UpdateAsync(UpdateProjectDto updateProject, CancellationToken cancellationToken = default)
        { 
            var group = await _groupRepository.GetAsync(updateProject.GroupId, cancellationToken);

            if (group == null)
            {
                throw new GroupNotFoundException($"Group with id: {updateProject.GroupId} not found", updateProject.GroupId);
            }
            var project = _mapper.Map<Project>(updateProject);

            await _repository.SaveChangesAsync(cancellationToken);

            var listProject = await _projectEmployeeRepository.Get().Where(x => x.Id == updateProject.Id).ToListAsync();
            _projectEmployeeRepository.Delete(listProject);

            SaveProjectEmployee(updateProject.Members, project, cancellationToken);

            return "Update Successfull";

        }
        private async void SaveProjectEmployee(String members, Project project, CancellationToken cancellationToken = default)
        {
            string[] Visas = members.Split(",");
            foreach (string member in Visas)
            {
                await Console.Out.WriteLineAsync(member);
                var employee = await _employeeRepository.Get()
                    .Where(x => member == x.Visa).FirstOrDefaultAsync();

                if(employee == null)
                {
                    throw new EmployeeNotFoundException($"Employee visa: {member} not found", member);
                }

                ProjectEmployee projectEmployee = new ProjectEmployee
                {
                    Project = project,
                    Employee = employee
                };

                await _projectEmployeeRepository.AddAsync(projectEmployee, cancellationToken);
                await _projectEmployeeRepository.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<Project>?> GetAll(CancellationToken cancellationToken = default)
        {
            var list = await _repository.GetValuesAsync(cancellationToken);

            return list;
        }

        public async Task<List<Project>?> Search(SearchProjectDto searchProject, CancellationToken cancellationToken = default)
        {
            var list =  _repository.Get();
            if (searchProject.Status != null)
            {
                list.Where(x => x.Status.Contains(searchProject.Status));
            }
            list.Where(x =>
                    x.ProjectNumber == searchProject.ProjectNumber ||
                    x.Customer.Contains(searchProject.Customer) ||
                    x.Name.Contains(searchProject.ProjectName)
                    );
            return await list.OrderBy(x=> x.ProjectNumber).ToListAsync();
        }

        public async Task<string> Delete(List<int> ids)
        {
            List<Project> projects = new();
            ids.ForEach( x =>
            {
                var entity = _repository.Get().Include(x => x.projectEmployees).FirstOrDefault();

                if(entity == null)
                {
                    throw new ProjectNotFoundException($"Project with id: {x} not found ", x);
                }

                projects.Add(entity);
            });

            _repository.Delete(projects);
            await _repository.SaveChangesAsync();

            return  "Delete successfull";
        }
    }

}