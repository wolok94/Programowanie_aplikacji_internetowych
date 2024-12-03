using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Interfaces.Services;

public interface IUserContextService
{
    ClaimsPrincipal User { get; }
    Guid? UserId { get; }
}
