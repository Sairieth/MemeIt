using MemeIt.Core;
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

    public async Task<Role?> GetRoleByIdAsync(long roleId)
    {
        return await _db.Roles.SingleOrDefaultAsync(r=>r.Id==roleId && r.DeletedOn.Equals(DateTime.MinValue));
    }
}