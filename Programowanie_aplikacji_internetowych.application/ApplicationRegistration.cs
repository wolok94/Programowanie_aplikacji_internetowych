using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Programowanie_aplikacji_internetowych.application.Services;
using Programowanie_aplikacji_internetowych.domain.Entities;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.application;

public static class ApplicationRegistration
{

    public static  IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


        return services;
    }
}
