using MemeIt.Data.Repositories.Interfaces;
using MemeIt.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MemeIt.Data.Repositories;

public class RoleRepository:IRoleRepository
{
    private readonly AppDbContext _db;

    public RoleRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Role>> GetRolesAsync()
    {
        return await _db.Roles.ToListAsync();
    }
}