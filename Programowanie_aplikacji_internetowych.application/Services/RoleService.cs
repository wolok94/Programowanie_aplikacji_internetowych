using Programowanie_aplikacji_internetowych.application.Interfaces.Services;
using Programowanie_aplikacji_internetowych.domain.Dtos.Roles;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<RoleDto>> GetRoles()
    {
        var roles = await _roleRepository.GetAll();
        var mappedRoles = roles.Select(x => new RoleDto
        {
            Name = x.Name,
            Id = x.Id,
        });

        return mappedRoles;
    }
}
