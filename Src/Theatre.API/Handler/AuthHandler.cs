using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Theatre.API.Handler
{
    public  class AuthHandler
    {
        private  readonly IConfiguration _config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        public  int IsValidToken(string jwtToken, string issuer, string audience, string securitykey)
        {

            return ValidateToken(jwtToken, issuer, audience, securitykey);
        }
        private  int ValidateToken(string jwtToken, string issuer, string audience, string securitykey)
        {

            try
            {
                byte[] bytekey = Encoding.UTF8.GetBytes(securitykey);
                SymmetricSecurityKey key = new SymmetricSecurityKey(bytekey);
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1),
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    IssuerSigningKey = key
                };
                ISecurityTokenValidator tokenValidator = new JwtSecurityTokenHandler();
                tokenValidator.ValidateToken(jwtToken, validationParameters, out
                    SecurityToken validatedToken);
                var jwtToken1 = (JwtSecurityToken)validatedToken;
                var scope = jwtToken1.Claims.First(c => c.Type.ToLower() == "name").Value;
                if (scope == null) return StatusCodes.Status404NotFound;
                return StatusCodes.Status200OK;
            }
            catch (Exception)
            {


                return StatusCodes.Status401Unauthorized;
            }
        }


        public  string GenerateTokenString(string UserName)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            SymmetricSecurityKey securitykey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                Subject = new ClaimsIdentity(new[] { new Claim("Name",UserName) }),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = credentials
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }


        public  ClaimsPrincipal GetToken(string token)
        {
            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwttoken = (JwtSecurityToken)handler.ReadToken(token);
                if (jwttoken == null)
                    return null;
                byte[] key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                TokenValidationParameters parameter = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securitytoken;
                ClaimsPrincipal principal = handler.ValidateToken(token, parameter, out securitytoken);
                return principal;
            }
            catch (Exception)
            {

                return null;
            }

        }
    }
}
