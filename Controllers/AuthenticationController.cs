using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApiWithEF.Models;

namespace WebApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        List<User> users = null;
        private readonly  IConfiguration _config ;
        public AuthenticationController(IConfiguration config)
        {
            _config = config;
           if(users==null)
            {
                users = new List<User>()
                {
                     new User(){UserId=101, UserName="user1",
                     Password="pass"},
                      new User(){UserId=102, UserName="user2",
                     Password="pass"}
                };
            } 
        }

        // GET: api/Authentication
        [HttpPost]
        public IActionResult GetUser(User user)
        {
            IActionResult response = Unauthorized();
            var obj = Authenticate(user);
            if(obj != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });

            }
            return response;
        }

        public User Authenticate(User user)
        {
            User obj = users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            return user;

        }
        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }






        private bool UserExists(int id)
        {
            return  users.Any(e => e.UserId == id);
        }
    }
}
