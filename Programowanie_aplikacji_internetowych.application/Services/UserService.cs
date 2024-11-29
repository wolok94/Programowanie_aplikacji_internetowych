using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Programowanie_aplikacji_internetowych.domain.Dtos.RefreshTokens;
using Programowanie_aplikacji_internetowych.domain.Dtos.Users;
using Programowanie_aplikacji_internetowych.domain.Entities;
using Programowanie_aplikacji_internetowych.domain.Exceptions;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Repository;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ITokenService _tokenService;

    public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task Register(RegisterUserDto registerUserDto)
    {
        if (registerUserDto.Password != registerUserDto.ConfirmPassword)
        {
            throw new ConfirmationPasswordException("Hasła się nie zgadzają");
        }

        var user = new User
        {
            Email = registerUserDto.Email,
            FirstName = registerUserDto.FirstName,
            LastName = registerUserDto.LastName,
            Username = registerUserDto.Username
        };
        user.Password = _passwordHasher.HashPassword(user, registerUserDto.Password);

        await _userRepository.AddAsync(user);
    }

    public async Task<Token> Login(LoginUserDto loginUserDto)
    {
        var user = await _userRepository.Login(loginUserDto.Password);
        if (user == null)
        {
            throw new ArgumentException("Nie ma takiego użytkownika");
        }

        var passwordIsVerified = _passwordHasher.VerifyHashedPassword(user, user.Password, loginUserDto.Password);
        if (passwordIsVerified == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed)
        {
            throw new ValidationPasswordException("Username lub hasło jest nieprawidłowe");
        }

        var token = await _tokenService.BuildToken(null, null, user);
        return token;




    }
}
