using Microsoft.EntityFrameworkCore;
using Programowanie_aplikacji_internetowych.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.Infrastructure.Seeders;

public class RoleSeeder
{
    private readonly AppDbContext _dbContext;

    public RoleSeeder(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedRoles()
    {
        if(! await _dbContext.Roles.AnyAsync())
        {
            var roles = await GetRoles();
            await _dbContext.Roles.AddRangeAsync(roles);
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task<IEnumerable<Role>> GetRoles()
    {
        var roles = new List<Role>{
            new Role
            {
                Name = "Admin"
            },
            new Role
            {
                Name = "User"
            }
        };

        return roles;
    }
}
