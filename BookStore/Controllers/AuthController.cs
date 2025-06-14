using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthController(IConfiguration configuration,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel(false) { Message = "User already exists!" });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                IsAdmin = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return Ok(new ResponseModel(false) { Message = "User creation failed! Please check user details and try again." + string.Join(", ", result.Errors.ToList().Select(x => x.Description)) });

            return Ok(new ResponseModel(true) { Message = "User created successfully!" });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new AuthResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    User = new UserResponse { Username = user.UserName, Email = user.Email, Id = user.Id, IsAdmin = user.IsAdmin},
                });
            }

            return Ok(new ResponseModel(false) { Message = "Username or Password incorrect" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
