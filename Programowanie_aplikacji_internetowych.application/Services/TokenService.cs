using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Programowanie_aplikacji_internetowych.domain.Dtos.RefreshTokens;
using Programowanie_aplikacji_internetowych.domain.Entities;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Services;
using Programowanie_aplikacji_internetowych.Infrastructure;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;

        public TokenService(IConfiguration configuration, AppDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<Token> BuildToken(string? accessToken, string? refreshToken, User user)
        {
            if (refreshToken != null && accessToken != null)
            {
                return await RefreshExistingToken(accessToken, refreshToken);
            }

            return await GenerateNewToken(user);
        }

        private async Task<Token> RefreshExistingToken(string accessToken, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(accessToken);
            var userId = Guid.Parse(principal.Identity.GetUserId());
            var oldRefreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (oldRefreshToken == null)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            var newRefreshTokenDto = GenerateRefreshToken();
            oldRefreshToken.Token = newRefreshTokenDto.Token;
            oldRefreshToken.ExpiryDate = DateTime.UtcNow.AddDays(30);
            oldRefreshToken.CreatedDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return CreateTokenResponse(principal.Claims, newRefreshTokenDto);
        }

        private async Task<Token> GenerateNewToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var newRefreshTokenDto = GenerateRefreshToken();
            var newRefreshToken = new RefreshToken
            {
                CreatedDate = newRefreshTokenDto.CreatedDate,
                ExpiryDate = newRefreshTokenDto.ExpiryDate,
                Token = newRefreshTokenDto.Token,
                UserId = user.Id
            };

            await _dbContext.RefreshTokens.AddAsync(newRefreshToken);
            await _dbContext.SaveChangesAsync();

            return CreateTokenResponse(claims, newRefreshTokenDto);
        }

        private Token CreateTokenResponse(IEnumerable<Claim> claims, RefreshTokenDto refreshTokenDto)
        {
            var accessToken = GenerateAccessToken(claims);

            return new Token
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = refreshTokenDto,
            };
        }

        private RefreshTokenDto GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return new RefreshTokenDto
            {
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(30),
                Token = Convert.ToBase64String(randomNumber)
            };
        }

        private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var expiryMinutes = Double.Parse(_configuration["JWT:ExpiryMinutes"]);

            return new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
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
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (!(securityToken is JwtSecurityToken jwtToken) ||
                !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
