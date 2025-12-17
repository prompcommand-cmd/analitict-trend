using AnaliticTrend.Application.Models;
using Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AnaliticTrend.Application.Utilities
{
    public class JwtTokenGenerator
    {
        private readonly JwtInternalSetting _jwtInternalSetting;

        public JwtTokenGenerator(IOptions<JwtInternalSetting> jwtInternalSetting)
        {
            _jwtInternalSetting = jwtInternalSetting.Value;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtInternalSetting.Key));

            var token = new JwtSecurityToken(
                audience: _jwtInternalSetting.Audience,
                expires: DateTime.UtcNow
                    .AddHours(_jwtInternalSetting.ExpireInHours)
                    .AddMinutes(_jwtInternalSetting.ExpireInMinutes)
                    .AddSeconds(_jwtInternalSetting.ExpireInSeconds),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenAsString;
        }

        public bool ValidateToken(string token)
        {
            return JwtTokenValidator.ValidateToken(token, _jwtInternalSetting.Key!, _jwtInternalSetting.Audience!);
        }
    }
}
