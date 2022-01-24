using MemeIt.Models.Entities;

namespace MemeIt.Data.Common;

public interface IRoleRepository
{
    Task<List<Role>> GetRolesAsync();
}