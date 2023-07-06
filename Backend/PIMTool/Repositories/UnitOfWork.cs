//using Microsoft.EntityFrameworkCore;
//using PIMTool.Core.Domain.Entities;
//using PIMTool.Core.Interfaces.Repositories;
//using PIMTool.Database;
//using System.Xml.Linq;

//namespace PIMTool.Repositories

//{
//    public class UnitOfWork : IUnitOfWork, IDisposable
//    {
//        private readonly PimContext _context;

//        private bool disposed = false;
//        public IRepository<Employee> EmployeeRepository { get; }

//        public IRepository<Project> ProjectRepository { get; }

//        public UnitOfWork(PimContext context, IRepository<Employee> employeeRepository, IRepository<Project> projectRepository)
//        {
//            _context = context;
//            EmployeeRepository = employeeRepository;
//            ProjectRepository = projectRepository;
//        }

//        public UnitOfWork()
//        {
//            _context = new PimContext(); // Instantiate your specific database context here
//            EmployeeRepository = new Repository<Employee>(_context);
//            ProjectRepository = new Repository<Project>(_context);
//            // Initialize other repositories as needed
//        }

//        public PimContext Context()
//        {
//            return _context;
//        }

//        public async Task SaveChangesAsync()
//        {
//           await _context.SaveChangesAsync();
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!disposed)
//            {
//                if (disposing)
//                {
//                    _context.Dispose();
//                }
//                disposed = true;
//            }
//        }
//    }
//}
