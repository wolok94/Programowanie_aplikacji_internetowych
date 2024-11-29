using Programowanie_aplikacji_internetowych.domain.Dtos.RefreshTokens;
using Programowanie_aplikacji_internetowych.domain.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Interfaces.Services;

public interface IUserService
{
    Task<Token> Login(LoginUserDto loginUserDto);
    Task Register(RegisterUserDto registerUserDto);
}
