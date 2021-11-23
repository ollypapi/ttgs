using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TTGS.Core.Interfaces;
using TTGS.Shared.Entity;
using TTGS.Shared.Request;
using TTGS.Shared.Response;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using TTGS.Shared.Settings;

namespace TTGS.Core.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<AspNetUsers> _signInManager;
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly ITTGSUnitOfWork _ttgsUnitOfWork;
        private readonly SystemSettings _appSettings;

        public UserService(IOptions<SystemSettings> appSettings,
            SignInManager<AspNetUsers> signInManager,
            UserManager<AspNetUsers> userManager,
            ITTGSUnitOfWork ttgsUnitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _ttgsUnitOfWork = ttgsUnitOfWork;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded) return null;

            var user = await _userManager.FindByEmailAsync(model.Username);

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = await generateJwtToken(user);
            var refreshToken = generateRefreshToken(ipAddress);

            // save refresh token
            user.RefreshTokens.Add(refreshToken);
            _ttgsUnitOfWork.AspNetUsers.Update(user);
            await _ttgsUnitOfWork.SaveAsync();
            var roles = await _userManager.GetRolesAsync(user);

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token, roles.ToArray());
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var user = _ttgsUnitOfWork.AspNetUsers.AsQueryable().SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = generateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            _ttgsUnitOfWork.AspNetUsers.Update(user);
            await _ttgsUnitOfWork.SaveAsync();
            var roles = await _userManager.GetRolesAsync(user);

            // generate new jwt
            var jwtToken = await generateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token, roles.ToArray());
        }

        private async Task<string> generateJwtToken(AspNetUsers user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }
    }
}
