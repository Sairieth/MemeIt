using MemeIt.Models.Entities;

namespace MemeIt.Data.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<List<Role>> GetRolesAsync();
}