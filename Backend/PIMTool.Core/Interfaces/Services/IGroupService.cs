using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IGroupService
    {
        Task<Boolean> AddAsync(AddGroup addGroup, CancellationToken cancellationToken = default);

        Task<Group?> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<Group?> UpdateAsync(UpdateGroup updateGroup, CancellationToken cancellationToken = default);

        Task<List<Group>> GetAllAsync(CancellationToken cancellationToken = default);

    }
}
