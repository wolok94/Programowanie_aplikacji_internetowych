﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Dtos.RefreshTokens;

public class Token
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiresAt { get; set; }
    public RefreshTokenDto RefreshToken { get; set; }
}
