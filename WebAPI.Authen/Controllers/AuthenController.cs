using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Authen.Controllers
{
    [Route("api/authen")]
    public class AuthenController : Controller
    {
        private readonly ILogger<AuthenController> _logger;

        public AuthenController(ILogger<AuthenController> logger)
        {
            _logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get(string name, string pwd)
        {
            if(name=="bonglv" && pwd == "123")
            {
                var now = DateTime.UtcNow;
                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,name),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,now.ToUniversalTime().ToString(),ClaimValueTypes.Integer64)
                };
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("bonglv@softty.net"));
                var tokenValidateionParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = key,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience=false
                };
                var jwt = new JwtSecurityToken(claims: claims,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var responeJson = new
                {
                    access_token=encodedJwt,
                    expires_im=(int)TimeSpan.FromMinutes(2).TotalSeconds
                };
                return Json(responeJson);
            }
            else
            {
                return Json("");
            }
        }
    }
}

