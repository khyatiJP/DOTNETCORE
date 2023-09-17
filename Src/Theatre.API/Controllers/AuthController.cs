using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Theatre.API.Handler;
using Theatre.Infrastructure.Provider;

namespace Theatre.API.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly RedisProvider _cache;

        public AuthController(RedisProvider cache)
        {
            _cache = cache;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult GenerateToken(string Username)
        {
            if (Username == "khyati")
            {
                AuthHandler authHandler = new AuthHandler();
                string token = authHandler.GenerateTokenString(Username);

                //object returnObject = new()
                //{
                //    token = token
                //    rtoken = GenerateRefreshToken()
                //};
                 _cache.SetString("Token", token);
                string value =_cache.GetString("Token");
                return new JsonResult(value);
            }
            return new JsonResult(("Please pass the valid Username and Password"));
        }
        

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
