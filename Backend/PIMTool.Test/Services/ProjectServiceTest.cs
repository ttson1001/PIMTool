using Microsoft.Extensions.DependencyInjection;
using PIMTool.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Test.Services
{
    public class ProjectServiceTest : BaseTest
    {
        private IProjectService _projectService = null!;

        [SetUp]
        public void SetUp()
        {
            _projectService = ServiceProvider.GetRequiredService<IProjectService>();
        }
        
    }
}
