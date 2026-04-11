using Identity_RefreshToken.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Identity_RefreshToken.Providers
{
    public class TokenService : ITokenServices
    {
        private readonly string _secretKey = "DayLaChiaKhoaBiMatSieuCapVoDichKhongTheBiHackDuoc123!!!";

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(string userName)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userName));
            var token = new JwtSecurityToken(issuer: "MyPocServer",
                audience: "MyPocClient",
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddMinutes(5));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetUserNameFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}