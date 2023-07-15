using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Theatre.API.Handler;

namespace Theatre.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController : Controller
    {
       // private readonly IDistributedCache _cache;

        //public AuthController(IDistributedCache cache)
        //{
        //    _cache = cache;
        //}
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
               // _cache.SetString("Token", JsonConvert.SerializeObject(token));
                return new JsonResult(token);
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
