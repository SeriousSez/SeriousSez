using Microsoft.IdentityModel.Tokens;
using SeriousSez.Domain.Entities;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SeriousSez.ApplicationService.Auth
{
    public interface IJwtFactory
    {
        SigningCredentials GetSigningCredentials();
        Task<List<Claim>> GetClaims(User user);
        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims, bool rememberMe = false);
    }
}
