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

namespace Programowanie_aplikacji_internetowych.application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var key = _configuration["Jwt:Key"];
        var expiry = _configuration["Jwt:ExpiryMinutes"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims, expires: DateTime.Now.AddMinutes(expiry)
            , signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
