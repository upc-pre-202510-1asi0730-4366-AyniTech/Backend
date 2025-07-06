﻿using System.Security.Claims;
using System.Text;
using Lot.IAM.Application.OutBoundServices;
using Lot.IAM.Domain.Model.Aggregates;
using Lot.IAM.Infrastructure.Tokens.JWT.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Lot.IAM.Infrastructure.Tokens.JWT.Services
{
    public class TokenService(IOptions<TokenSettings> tokenSettings) : ITokenService
    {
        private readonly TokenSettings _tokenSettings = tokenSettings.Value;

        public string GenerateToken(User user)
        {
            var secret = _tokenSettings.Secret;
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JsonWebTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }

        /**
         * <summary>
         *     VerifyPassword token
         * </summary>
         * <param name="token">The token to validate</param>
         * <returns>The user id if the token is valid, null otherwise</returns>
         */
        public async Task<int?> ValidateToken(string token)
        {
            // If token is null or empty
            if (string.IsNullOrEmpty(token))
                // Return null 
                return null;
            // Otherwise, perform validation
            var tokenHandler = new JsonWebTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            try
            {
                var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // Expiration without delay
                    ClockSkew = TimeSpan.Zero
                });

                var jwtToken = (JsonWebToken)tokenValidationResult.SecurityToken;
                var userId = int.Parse(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value);
                return userId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}

