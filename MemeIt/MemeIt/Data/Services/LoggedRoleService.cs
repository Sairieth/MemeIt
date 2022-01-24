using MemeIt.Data.Common;
using MemeIt.Models.Entities;

namespace MemeIt.Data.Services;

public class LoggedRoleService:IRoleRepository
{
    public Task<List<Role>> GetRolesAsync()
    {
        throw new NotImplementedException();
    }
}