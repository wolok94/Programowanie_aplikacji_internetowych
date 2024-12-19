using Programowanie_aplikacji_internetowych.domain.Dtos.Roles;
using Programowanie_aplikacji_internetowych.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Dtos.Users;

public class GetUsersDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public RoleDto Role { get; set; }
}
