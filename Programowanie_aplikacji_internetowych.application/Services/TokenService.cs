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
using System.Security.Cryptography;
using Programowanie_aplikacji_internetowych.domain.Dtos.RefreshTokens;
using Programowanie_aplikacji_internetowych.Infrastructure;

namespace Programowanie_aplikacji_internetowych.application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _dbContext;

    public TokenService(IConfiguration configuration, AppDbContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }

    public async Task<Token> BuildToken(string? accessToken, string? refreshToken ,User user)
    {
        JwtSecurityToken tokenDescriptor;
        Guid userId;

        if (refreshToken != null && accessToken != null)
        {
            var principal = GetPrincipalFromExpiredToken(accessToken);
            userId = Guid.Parse(principal.Identity.GetUserId());
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
            userId = user.Id;
            tokenDescriptor = GenerateAccessToken(claims);
        }

        var newRefreshTokenDto = GenerateRefreshToken();
        var newRefreshToken = new RefreshToken
        {
            CreatedDate = newRefreshTokenDto.CreatedDate,
            ExpiryDate = newRefreshTokenDto.ExpiryDate,
            Token = newRefreshTokenDto.Token,
            UserId = userId
        };
        await _dbContext.RefreshTokens.AddAsync(newRefreshToken);
        await _dbContext.SaveChangesAsync();

        var token = new Token
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor),
            RefreshToken = newRefreshTokenDto,
        };


        return token;
    }

    private RefreshTokenDto GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            var token = new RefreshTokenDto
            {
                CreatedDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(30),
                Token = Convert.ToBase64String(randomNumber)
            };
            return token;
        }
    }

    private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = _configuration["JWT:Key"];
        var expiry = _configuration["JWT:ExpiryMinutes"];
        var issuer = _configuration["JWT:Issuer"];
        var audience = _configuration["JWT:Audience"];

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null ||
        !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}
