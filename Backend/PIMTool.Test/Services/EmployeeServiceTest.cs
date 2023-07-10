using Microsoft.Extensions.DependencyInjection;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects.Employee;
using PIMTool.Core.Exceptions.Employee;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Test.Services
{
    [TestFixture]
    public class EmployeeServiceTest : BaseTest
    {
        private IEmployeeService _employeeService = null!;

        [SetUp]
        public void SetUp()
        {
            _employeeService = ServiceProvider.GetRequiredService<IEmployeeService>();
        }

        [Test]
        public async Task Test_GetAll_WithSizeValue()
        {
            int count = 4;

            var es = await _employeeService.GetAll();

            Assert.AreEqual(count, es.Count());
        }

        [Test]
        public async Task Test_AddAsync_Successfully()
        {
            var employee = new AddEmployee()
            {
                Visa = "TTS",
                FirstName = "Tran Thanh",
                LastName = "Son",
                Birthday = DateTime.Parse("2000-11-26"),
            };


      
        }

        [Test]
        public async Task Test_AddAsync_BirthdayExcetion()
        {
            var employee = new AddEmployee()
            {
                Visa = "TTS",
                FirstName = "Tran Thanh",
                LastName = "Son",
                Birthday = DateTime.Now,
            };

            Assert.ThrowsAsync<BirthDayException>(async () => await _employeeService.AddAsync(employee));
        }
    }
}
