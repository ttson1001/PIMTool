using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Database;
using PIMTool.Extensions;

namespace PIMTool.Test
{
    public abstract class BaseTest
    {
        //private static DbContextOptions<PimContext> dbContextOptions = new DbContextOptionsBuilder<PimContext>()
        //      .UseInMemoryDatabase(databaseName: "PIMDBTest")
        //      .Options;
        //public PimContext context;
        protected PimContext Context { get; private set; } = null!;
        protected IServiceProvider ServiceProvider { get; private set; } = null!;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<PimContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.Register();
            ServiceProvider = services.BuildServiceProvider();
            Context = ServiceProvider.GetRequiredService<PimContext>();
            SeedDatabase();
        }

        [TearDown]
        public void TearDown()
        {
            Context.Dispose();
        }


        private void SeedDatabase()
        {
            var employees = new List<Employee>()
            {
                new Employee()
                {
                    Visa = "NMH",
                    FirstName = "Nguyen Minh",
                    LastName = "Hieu",
                    Birthday = DateTime.Today,
                },
                 new Employee()
                {
                    Visa = "HHT",
                    FirstName = "Ho Huu",
                    LastName = "Thong",
                    Birthday = DateTime.Today,
                },
                  new Employee()
                {
                    Visa = "VVS",
                    FirstName = "Vu Viet",
                    LastName = "Sang",
                    Birthday = DateTime.Today,
                },
                   new Employee()
                {
                    Visa = "HTV",
                    FirstName = "Ha Thanh",
                    LastName = "Vi",
                    Birthday = DateTime.Today,
                }

            };

            Context.Employees.AddRange(employees);
            Context.SaveChanges();
        }
    }
}