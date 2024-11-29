using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Token { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
}
