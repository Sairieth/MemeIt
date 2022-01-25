using MemeIt.Models.Entities;

namespace MemeIt.Core;

public interface IRoleRepository
{
    Task<List<Role>> GetRolesAsync();
}