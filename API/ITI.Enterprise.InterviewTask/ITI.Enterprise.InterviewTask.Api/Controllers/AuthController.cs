using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ITI.Enterprise.InterviewTask.Api.DTO;
using ITI.Enterprise.InterviewTask.DomainClasses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ITI.Enterprise.InterviewTask.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="config"></param>
        public AuthController(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        /// <summary>
        /// Registration method.
        /// </summary>
        /// <param name="userToRegister">User Object</param>
        /// <returns>An ActionResult - Created or BadRequest- depending on the if the user was registered or not if the data is not valid.</returns>
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(UserDto userToRegister)
        {
            IActionResult actionResult;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = new User
            {
                UserName = userToRegister.Email,
                Email = userToRegister.Email
            };
            var result = await _userManager.CreateAsync(user, userToRegister.Password);
            if (result.Succeeded)
            {
                actionResult = Created("", null);
            }
            else
            {
                actionResult = BadRequest();
            }

            return actionResult;
        }

        /// <summary>
        /// Token issuing for registered users.
        /// </summary>
        /// <param name="user">User Object</param>
        /// <returns>An ActionResult - Ok or Unauthorized - depending on if the user is valid or not.</returns>
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(UserDto user)
        {
            IActionResult actionResult;
            if (!ModelState.IsValid)
            {
                actionResult = BadRequest(ModelState);
                return actionResult;
            }

            var userFromDb = await _userManager.FindByEmailAsync(user.Email);
            if (userFromDb == null)
            {
                actionResult = Unauthorized();
            }
            else
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,userFromDb.Id),
                    new Claim(ClaimTypes.Name,userFromDb.Email), 
                };
                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = credentials
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                actionResult = Ok(new {token = tokenHandler.WriteToken(token)});
            }
            return actionResult;
        }
    }
}