using Programowanie_aplikacji_internetowych.domain.Dtos.RefreshTokens;
using Programowanie_aplikacji_internetowych.domain.Dtos.Users;
using Programowanie_aplikacji_internetowych.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Interfaces.Services;

public interface IUserService
{
    Task ChangeRole(ChangeRoleForUserDto changRole);
    Task<IEnumerable<GetUsersDto>> GetUsers();
    Task<Token> Login(LoginUserDto loginUserDto);
    Task<Token> RefreshToken(string accessToken, string refreshToken);
    Task Register(RegisterUserDto registerUserDto);
}
