using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
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

    public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
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

    public async Task Login(LoginUserDto loginUserDto)
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




    }
}
