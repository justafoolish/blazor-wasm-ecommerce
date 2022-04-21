using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlazorAppEC.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using BlazorAppEC.Shared.Authentication;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace BlazorAppEC.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private BlazorECContext _appContext;

        private readonly ILogger<UserController> _logger;
        private readonly Utility _utility;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, BlazorECContext appContext, IConfiguration configuration)
        {
            _logger = logger;
            _appContext = appContext;
            _utility = new Utility();
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(User user)
        {
            User isEmailExists = _appContext.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
            bool success = false;
            if (isEmailExists == null)
            {
                user.Password = _utility.Encrypt(user.Password);
                _appContext.Users.Add(user);
                success = await _appContext.SaveChangesAsync() != 0 ? true : false;
            }

            return success ? Ok(new {success = success, message = "Register successfully"}) : BadRequest(new {success = false, message = "Can not register"});
        }

        [HttpPost("authjwt")]
        public async Task<ActionResult<AuthenticationResponse>> authenticationJWT(AuthenticationRequest user)
        {
            string token = string.Empty;

            user.Password = _utility.Encrypt(user.Password);
            User loggedInUser = await _appContext.Users
                    .Where(u => u.Email == user.Email && u.Password == user.Password)
                    .FirstOrDefaultAsync();
            if (loggedInUser != null)
            {
                token = generateJWT(loggedInUser);
            }

            return await Task.FromResult(new AuthenticationResponse() { Token = token });
        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUser(AuthenticationRequest user)
        {
            string token = string.Empty;
            User loggedInUser = await _appContext.Users.Where(u => u.Email.Equals(user.Email) && u.Password.Equals(user.Password)).FirstOrDefaultAsync();

            if (loggedInUser != null)
            {
                var claim = new Claim(ClaimTypes.Name, loggedInUser.Email);

                var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);
            }

            return await Task.FromResult(loggedInUser);
        }

        [HttpGet("logout")]
        public async Task<ActionResult<string>> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }

        [HttpGet("session")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            User currentUser = new User();
            if (User.Identity.IsAuthenticated)
            {
                currentUser.Email = User.FindFirstValue(ClaimTypes.Name);
            }
            return await Task.FromResult(currentUser);
        }
        [HttpPost("getuserbyjwt")]
        public async Task<ActionResult<User>> GetUserByJWT([FromBody] string jwtToken)
        {
            try
            {
                //getting the secret key
                string secretKey = _configuration["JWTSettings:SecretKey"];
                var key = Encoding.ASCII.GetBytes(secretKey);

                //preparing the validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;

                //validating the token
                var principle = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = (JwtSecurityToken)securityToken;

                if (jwtSecurityToken != null
                    && jwtSecurityToken.ValidTo > DateTime.Now
                    && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    //returning the user if found
                    var userId = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    return await GetUserByUserID(Convert.ToInt64(userId));
                }
            }
            catch (Exception ex)
            {
                //logging the error and returning null
                Console.WriteLine("Exception : " + ex.Message);
                return null;
            }
            //returning null if token is not validated
            return null;
        }
        protected async Task<User> GetUserByUserID(long userId)
        {
            return await _appContext.Users.Select(user => new User
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email,
                Address = user.Address,
                Role = user.Role,
            }).Where(user => user.UserId == userId).FirstOrDefaultAsync();
        }
        private string generateJWT(User user)
        {
            string secretKey = _configuration["JWTSettings:SecretKey"];
            var key = Encoding.ASCII.GetBytes(secretKey);

            //create claims
            var claimEmail = new Claim(ClaimTypes.Email, user.Email);
            var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString());
            var claimName = new Claim(ClaimTypes.Name, user.Fullname);
            var claimAddress = new Claim(ClaimTypes.StreetAddress, user.Address);
            var claimRole = new Claim(ClaimTypes.Role, user.Role == null ? "" : user.Role);

            //create claimsIdentity
            var claimsIdentity = new ClaimsIdentity(new[] { claimEmail, claimNameIdentifier, claimName, claimAddress, claimRole }, "serverAuth");

            // generate token that is valid for 7 days
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //creating a token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //returning the token back
            return tokenHandler.WriteToken(token);
        }
    }
}
