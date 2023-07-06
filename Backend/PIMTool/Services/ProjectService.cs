using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects.Project;
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

        public ProjectService(
            IRepository<Project> repository,
            IRepository<Group> groupRepository,
            IRepository<Employee> employeeRepository,
            IRepository<ProjectEmployee> projectEmployeeRepository)
        {
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


        public async Task<string> AddAsync(AddProject addProject, CancellationToken cancellationToken = default)
        {
            var group = await _groupRepository.GetAsync(addProject.GroupId, cancellationToken);

            Project project = new Project
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

        public async Task<string> UpdateAsync(UpdateProject updateProject, CancellationToken cancellationToken = default)
        { 
            var group = await _groupRepository.GetAsync(updateProject.GroupId, cancellationToken);

            var project = await _repository.GetAsync(updateProject.Id, cancellationToken);

            project.Name = updateProject.Name;
            project.Customer = updateProject.Customer;
            project.Status = updateProject.Status;
            project.StartDate = updateProject.StartDate;
            project.EndDate = updateProject.EndDate;
            project.Group = group;

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
                var employee = await _employeeRepository.Get()
                    .Where(x => member == x.Visa).FirstOrDefaultAsync();
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

        public async Task<List<Project>?> Search(SearchProject searchProject, CancellationToken cancellationToken = default)
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
                    )
                ;

            return await list.ToListAsync();
        }
    }

}