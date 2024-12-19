using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Entities;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<User> Users { get; set; }
}
