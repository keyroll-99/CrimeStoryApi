using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Authentication.Contracts;
using Authentication.Contracts.Models;
using Authentication.Core.Entities;
using Authentication.Core.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthenticationService(IOptions<JwtSettings> jwtSettings, IRefreshTokenRepository refreshTokenRepository)
    {
        _jwtSettings = jwtSettings.Value;
        _refreshTokenRepository = refreshTokenRepository;
    }
    
    public long? ValidateJwtToken(string? token)
    {
        if (token is null)
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
                
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            return int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public string GenerateJwtToken(long userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
            Expires = DateTime.UtcNow.AddMinutes(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<string> GenerateRefreshToken(long userId, string ipAddress)
    {
        var refreshToken = new RefreshToken
        {
            Token = await GetNewRefreshToken(),
            ExpireDate = DateTime.UtcNow.AddDays(7),
            CreatedBy = ipAddress,
            UserId = userId
        };

        await _refreshTokenRepository.AddAsync(refreshToken);

        return refreshToken.Token;
    }

    public async Task RevokeAllRefreshToken(long userId, string ipAddress)
    {
        var userTokens = await _refreshTokenRepository.GetAllUserRefreshTokensAsync(userId);

        foreach (var userToken in userTokens)
        {
            userToken.IsUsed = true;
            userToken.UsedBy = ipAddress;
        }

        await _refreshTokenRepository.UpdateRangeAsync(userTokens);
    }

    public async Task RevokeRefreshToken(string token, string ipAddress)
    {
        var refreshToken = await _refreshTokenRepository.GetRefreshTokenByTokenAsync(token);
        refreshToken.UsedBy = ipAddress;
        refreshToken.IsUsed = true;

        await _refreshTokenRepository.UpdateAsync(refreshToken);
    }

    public async Task<bool> IsValidRefreshToken(string token)
    {
        var refreshToken = await _refreshTokenRepository.GetRefreshTokenByTokenAsync(token);

        return !refreshToken.IsUsed && (DateTime.UtcNow < refreshToken.ExpireDate);
    }

    private async Task<string> GetNewRefreshToken()
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        if (await _refreshTokenRepository.TokenExistsAsync(token))
        {
            return await GetNewRefreshToken();
        }

        return token;
    }
}