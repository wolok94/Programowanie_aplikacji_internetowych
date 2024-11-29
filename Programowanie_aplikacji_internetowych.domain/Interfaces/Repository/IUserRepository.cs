using Programowanie_aplikacji_internetowych.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Interfaces.Repository;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> Login(string username);
}
