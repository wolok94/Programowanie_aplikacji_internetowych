using Microsoft.AspNet.Identity;
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
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
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
            Password = _passwordHasher.HashPassword(registerUserDto.Password),
            Email = registerUserDto.Email,
            FirstName = registerUserDto.FirstName,
            LastName = registerUserDto.LastName,
            Username = registerUserDto.Username
        };

        await _userRepository.AddAsync(user);
    }
}
