using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Programowanie_aplikacji_internetowych.domain.Entities;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Programowanie_aplikacji_internetowych.application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string BuildToken(string? accessToken, string? refreshToken ,User user)
    {
        JwtSecurityToken tokenDescriptor;

        if (refreshToken != null && accessToken != null)
        {
            var principal = GetPrincipalFromExpiredToken(accessToken);
            var userId = principal.Identity.GetUserId();
            tokenDescriptor = GenerateAccessToken(principal.Claims);
        }
        else
        {
            var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };

            tokenDescriptor = GenerateAccessToken(claims);
        }


        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = _configuration["Jwt:Key"];
        var expiry = _configuration["Jwt:ExpiryMinutes"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims, expires: DateTime.Now.AddMinutes(Double.Parse(expiry))
            , signingCredentials: credentials);

        return tokenDescriptor;
    }

    private  ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null ||
    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}
