using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CaseStudyAppServer.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly ITokenRepository _tokenRepo;
        private readonly UserManager<AppUser> _userManager;
        public TokenService(IConfiguration config, ITokenRepository tokenRepo, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _tokenRepo = tokenRepo;
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]!));
        }
        public async Task<Token?> CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Email, user.Email!),
                new (JwtRegisteredClaimNames.GivenName, user.UserName!),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var signing = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
            DateTime expiration = DateTime.UtcNow.AddDays(7);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiration,
                SigningCredentials = signing,
                Issuer = _config["JWT:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            string writtenToken = tokenHandler.WriteToken(token);

            string refreshToken = GenerateRefreshToken();

            //SINGLE SIGNON PREVIOUS TOKENS ARE DELETED   
            var createdToken = await _tokenRepo.CreateAndDeletePreviousAsync(user.Id, writtenToken, refreshToken, expiration);

            return createdToken;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        

    }
}