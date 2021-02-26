
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OneStopApp.Interface;
using OneStopApp_Api.EntityFramework.Data;
using OneStopApp_Api.EntityFramework.ViewModel;
using OneStopApp_Api.Enums;
using OneStopApp_Api.Interface;
using OneStopApp_Api.ViewModel;

namespace OneStopApp.Service
{
    public class AuthService : IAuthService
    {
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly OsaDbContext _context;
        private readonly ISaltPasswordService _saltPasswordService;
        private readonly IOptions<PasswordChangeSettings> _passwordChangeSettings;
        public AuthService(OsaDbContext context, ISaltPasswordService SaltedPasswordService)
        {
            _context = context;
            _saltPasswordService = SaltedPasswordService;
        }
        public TokenResultViewModel DoPassword(JwtParameterViewModel model)
        {
            var loginViewModel = new LoginViewModel
            {
                UserName = model.Username,
                Password = model.Password
            };
            dynamic result;
            result = AuthenticateUser(loginViewModel).Result;

            if (result.Result == AuthResult.Success)
            {
                return new TokenResultViewModel
                {
                    Code = "1000",
                    Message = "Authenticated Successfully",
                    Data = GetJwt(model.ClientId, result)
                };
            }
            else if (result.Result == AuthResult.AccountLocked)
            {
                return new TokenResultViewModel
                {
                    Code = "3000",
                    Message = "Account locked.",
                    Data = null
                };
            }
            else
            {
                return new TokenResultViewModel
                {
                    Code = "2000",
                    Message = "Invalid Username or Password",
                    Data = null
                };
            }
        }
        public TokenResultViewModel DoRefreshToken(JwtParameterViewModel model)
        {
            var oldToken = new JwtSecurityTokenHandler().ReadJwtToken(model.RefreshToken);
            if (oldToken.ValidTo > DateTime.UtcNow)
            {
                return new TokenResultViewModel
                {
                    Code = "4000",
                    Message = "Token has refreshed successfully",
                    Data = GetJwt(oldToken.Subject, null)
                };
            }
            else
            {
                return new TokenResultViewModel
                {
                    Code = "5000",
                    Message = "Refresh token has expired",
                    Data = null
                };
            };
        }

        private string GetJwt(string clientId, dynamic loginData)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, clientId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var symmetricKeyAsBase64 = _jwtSettings.Value.SecretKey;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Value.Issuer,
                audience: _jwtSettings.Value.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(double.Parse(_jwtSettings.Value.AccessTokenExpirationTime))),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedAccessJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Value.Issuer,
                audience: _jwtSettings.Value.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(double.Parse(_jwtSettings.Value.RefreshTokenExpirationTime))),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedRefreshJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedAccessJwt,
                expires_in = (int)TimeSpan.FromMinutes(double.Parse(_jwtSettings.Value.AccessTokenExpirationTime)).TotalSeconds,
                refresh_token = encodedRefreshJwt,
                userDetails = loginData
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        private async Task<LoginViewModel> AuthenticateUser(LoginViewModel model)
        {
            var userResponse = new LoginViewModel() { Result = AuthResult.Failed };
            var user = await _context.Users.Where(usr => usr.UserName == model.UserName).FirstOrDefaultAsync();
            if (user == null)
            {
                return userResponse;
            }

            var profile = await _context.Profiles.Where(ity => ity.UserId == user.Id).FirstOrDefaultAsync();
            var passwordMatch = _saltPasswordService.ComparePasswords(user.Password, model.Password);
            userResponse.UserId = user.Id;
            userResponse.UserName = user.UserName;
            userResponse.FullName = profile != null ? string.Format("{0} {1} {2}", profile.FirstName, profile.MiddleName, profile.LastName) : string.Empty;
            userResponse.PasswordMatch = passwordMatch;
            userResponse.UserStatusId = user.StatusId;
            userResponse.UserRole = _context.UserRoles.Where(ur => ur.UserId == user.Id).First().Roles.Name;
            if (user.StatusId == (int)UserStatusEnum.Active)
            {
                if (passwordMatch)
                {
                    userResponse.Result = AuthResult.Success;
                }

                await _context.SaveChangesAsync();
                userResponse.UserStatusId = user.StatusId;
            }
            else if (user.StatusId == (int)UserStatusEnum.Locked)
            {
                userResponse.Result = AuthResult.AccountLocked;
            }
            return userResponse;
        }
    }
};