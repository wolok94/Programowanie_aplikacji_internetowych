using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Dtos.Users;

public class ChangeRoleForUserDto
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
