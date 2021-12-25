using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; } //appsettingsteki degerleri okumamıza yarar
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            //TokenOptionstaki degerleri alarak mapler (appsettingsteki TokenOptions, TokenOptions sınıfındaki degerlerle eslesir)
            //GetSection: TokenOptions, Logging kısımlarını ifade eder.
        }
        //user ve claimlere gore token uretecek metot
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            var refreshToken = CreateRefreshToken();

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
                RefreshToken = refreshToken
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        //Refresh Token üretecek metot.
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        //Claim: sisteme giris yapan kullanıcının bilgilerini tutar
        // Extensionlar ile bir nesneye yeni metotlar ekleyebiliyoruz
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString()); //Kullanıcının idsi
            claims.AddEmail(user.Email); // email
            claims.AddName($"{user.FirstName} {user.LastName}"); //adı soyadı
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray()); //kullanıcının veritabanından cektigimiz claimlerinin(rollerinin) adını cekip rollere basıyoruz

            return claims;
        }
    }
}
