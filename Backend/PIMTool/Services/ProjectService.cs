﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Dtos.ProjectDtos.Request;
using PIMTool.Core.Exceptions.Employee;
using PIMTool.Core.Exceptions.Group;
using PIMTool.Core.Exceptions.Project;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;

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

            if (null == entity)
            {
                throw new ProjectNotFoundException($"Project with id: {id} not found ", id);
            }
            return entity;
        }

        public async Task<Boolean> AddAsync(RequestProjectDto projectAddDto, CancellationToken cancellationToken = default)
        {
            var group = await _groupRepository.GetAsync(projectAddDto.GroupId, cancellationToken);
            if (null == group)
            {
                throw new GroupNotFoundException($"Group with id: {projectAddDto.GroupId} not found", projectAddDto.GroupId);
            }
            var checkPojectNumber = _repository.Get().Where(x => x.ProjectNumber == projectAddDto.ProjectNumber).SingleOrDefault();
            if (checkPojectNumber != null)
            {
                throw new ProjectDupicateNumberException($"Project number {projectAddDto.ProjectNumber} is dupplicate");
            }
            var project = _mapper.Map<Project>(projectAddDto);
            project.Group = group;
            await _repository.AddAsync(project, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            _repository.ClearChangeTracking();

            if (null != projectAddDto.Members)
            {
                await SaveProjectEmployee(projectAddDto.Members, projectAddDto.ProjectNumber, cancellationToken);
            }
            return true;

        }

        public async Task<Project?> UpdateAsync(RequestProjectDto ProjectUpdateDto, CancellationToken cancellationToken = default)
        {
            var group = await _groupRepository.GetAsync(ProjectUpdateDto.GroupId, cancellationToken);

            if (null == group)
            {
                throw new GroupNotFoundException($"Group with id: {ProjectUpdateDto.GroupId} not found", ProjectUpdateDto.GroupId);
            }

            var project = await _repository.GetAsync(ProjectUpdateDto.Id, cancellationToken);

            if (null == project)
            {
                throw new ProjectNotFoundException($"Project with id: {ProjectUpdateDto.Id} not found ", ProjectUpdateDto.Id);
            }

            _mapper.Map(ProjectUpdateDto, project);
            _repository.Update(project);
            await _repository.SaveChangesAsync(cancellationToken);

            var listProject = await _projectEmployeeRepository.Get().Where(x => x.ProjectId == ProjectUpdateDto.Id).ToListAsync();
            _projectEmployeeRepository.Delete(listProject);

            if (null != ProjectUpdateDto.Members)
            {
                await SaveProjectEmployee(ProjectUpdateDto.Members, ProjectUpdateDto.ProjectNumber, cancellationToken);
            }

            return await _repository.GetAsync(ProjectUpdateDto.Id, cancellationToken);
        }

        private async Task SaveProjectEmployee(String members, int projectNumber, CancellationToken cancellationToken = default)
        {
            string[] Visas = members.Split(",");
            var project = _repository.Get().Where(x => x.ProjectNumber == projectNumber).SingleOrDefault();
            foreach (string member in Visas)
            {
                await Console.Out.WriteLineAsync(member);
                var employee = await _employeeRepository.Get()
                    .Where(x => member.Equals(x.Visa)).FirstOrDefaultAsync();
                if (null == employee)
                {
                    throw new EmployeeNotFoundException($"Employee visa: {member} not found", member);
                }
                ProjectEmployee projectEmployee = new()
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
            return await _repository.GetValuesAsync(cancellationToken);
        }

        public async Task<List<Project>?> Search(ProjectSearchDto searchProject, CancellationToken cancellationToken = default)
        {
            var query = _repository.Get();
            if (searchProject.Status != null)
            {
                query = query.Where(x => x.Status.Contains(searchProject.Status));
            }
            if (searchProject.SearchValue != null)
            {
                query = query.Where(x =>
                        x.ProjectNumber.ToString() == searchProject.SearchValue ||
                        x.Customer.Contains(searchProject.SearchValue) ||
                        x.Name.Contains(searchProject.SearchValue)
                     );
            }
            return await query.OrderBy(x => x.ProjectNumber).ToListAsync(); ;
        }

        public async Task<Boolean> Delete(List<int> ids)
        {
            List<Project> projects = new();
            ids.ForEach(x =>
            {
                var entity = _repository.Get().Include(x => x.projectEmployees).FirstOrDefault();
                if (null == entity)
                {
                    throw new ProjectNotFoundException($"Project with id: {x} not found ", x);
                }
                projects.Add(entity);
            });
            _repository.Delete(projects);
            await _repository.SaveChangesAsync();
            return true;
        }

        public Boolean CheckProjectNumber(int projectNumber)
        {
            var project = _repository.Get().Where(x => x.ProjectNumber == projectNumber).FirstOrDefault();
            if (null == project)
            {
                return true;
            }
            return false;
        }
    }

}