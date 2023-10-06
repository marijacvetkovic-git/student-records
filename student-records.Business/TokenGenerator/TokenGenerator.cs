using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using student_records.Business.DTOs.JWT;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace student_records.Business.TokenGenerator
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JWTResultsDTO GenerateJWTToken(JWTCreateDTO user, DateTime expires)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                authClaims,
                expires: expires,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
            );

            JWTResultsDTO result = new()
            {
                Token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token),
                Expires = token.ValidTo,
                UserId = user.Id,
                Role = user.Role
            };

            return result;
        }
    }
}
