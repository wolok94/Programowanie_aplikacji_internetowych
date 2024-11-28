using Programowanie_aplikacji_internetowych.domain.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Interfaces.Services;

public interface IUserService
{
    Task Register(RegisterUserDto registerUserDto);
}
