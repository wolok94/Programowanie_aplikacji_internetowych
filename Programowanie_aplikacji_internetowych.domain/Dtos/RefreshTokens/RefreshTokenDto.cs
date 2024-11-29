using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Dtos.RefreshTokens;

public class RefreshTokenDto
{
    public DateTime ExpiryDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Token { get; set; }
}
