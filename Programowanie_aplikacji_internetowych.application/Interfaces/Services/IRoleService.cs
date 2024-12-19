using Programowanie_aplikacji_internetowych.domain.Dtos.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.application.Interfaces.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetRoles();
}
