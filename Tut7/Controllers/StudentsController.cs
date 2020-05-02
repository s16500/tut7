using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tut7.Models;

namespace Tut7.Controllers
{

    [ApiController]
    [Route("api/[controller]")]


    public class StudentsController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        public StudentsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetStudents()
        {
            return Ok("secret string");
        }

        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            var claims = new[]
          {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "Mustafa"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "everyone")
            };

            var key = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Mustafa",
                audience: "everyone",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds

             );

            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()
            }
                ); ;

        }

        [HttpPost("refresh-token/{token}")]
        public IActionResult RefreshToken(string requestToken)
        {

            return Ok();
        }
    }

}