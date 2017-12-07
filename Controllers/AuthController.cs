using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using windforce_corp.Models;
using windforce_corp.ViewModels;

namespace windforce_corp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DateTime expirationTime = DateTime.Now.AddDays(1);
        
        public AuthController
        (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AuthController> logger,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
            
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"{user.Email} logged in.");
                        var token = BuildToken(user.Id ,user.Email, expirationTime);

                        return Ok(new 
                        { 
                            message = "log in successful",
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            user = MapUser(user),
                        });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Login credentials");
                        return BadRequest(new { message = "unable to sign user in ", errors = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(x => x.ErrorMessage) });
                    }
                }
                return BadRequest(new { message = "Invalid Login credentials" });
            }
            
            return BadRequest(new { message = "Oops, you seem to have entered some invalid data", errors = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(x => x.ErrorMessage)});
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody]SignUpViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { 
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"User {user.UserName} created a new account with password.");

                    var token = BuildToken(user.Id ,user.Email, expirationTime);

                    return Ok(new 
                    { 
                        message = "sign up successful",
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        user = MapUser(user),
                    });
                }
                AddErrors(result);
                return BadRequest(new { message = "Sorry, We're unable to sign you up ", errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(x => x.ErrorMessage) });
            }
           return BadRequest(new { message = "Oops, you seem to have entered some invalid data", errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(x => x.ErrorMessage) });
        }


        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        private UserViewModel MapUser(ApplicationUser user)
        {
            return new UserViewModel() 
            { 
              Id = user.Id,
              Email = user.Email,
              FirstName = user.FirstName,
              LastName = user.LastName,
              FullName = user.FullName
            };
        }
        private JwtSecurityToken BuildToken(string userId, string email, DateTime expirationTime) 
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim("Email", email),
                new Claim("UserId", userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
    
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"],
                claims,
                expires: expirationTime,
                signingCredentials: creds);
            _logger.LogInformation($"Generated token for {email}");
            
            return token;
        }

        #endregion
    }
    
}
