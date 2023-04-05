using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.data;
using UserApi.models;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // api/auth
    public class AuthController: ControllerBase
    {
        private readonly IdentityDbContext _db;

        private readonly IConfiguration _configuration;
        public AuthController(IdentityDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
    

    // POST /api/auth/register
    [HttpPost("register", Name ="RegisterUser")]
    public async Task<ActionResult> ResisterAsync([FromBody]User user)
    {
            TryValidateModel(user);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return Created("", new UserResponseModel()
                {
                    Id=user.Id, Email=user.Email, Fullname=user.Fullname, Mobile=user.Mobile
                });
            }
    }
        // POST /api/auth/token
        [HttpPost("login", Name = "Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(TokenModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
            TryValidateModel(loginModel);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _db.Users.SingleOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
            if (user == null)
                return Unauthorized();
            var token = GenerateToken(user);
            return Ok(new TokenModel()
            {
                Token = token,
                Fullname = user.Fullname,
                Email = user.Email,
            });
        }

        [NonAction]
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Fullname),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, "product"),
                new Claim(JwtRegisteredClaimNames.Aud, "moviesApi"),
            };


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Secret")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: null ,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
        
            // Token Structure:
            /*
             * Visit jwt.io for token structure details
             * Header
             * Payload
             * Signature
             * 
             * What should a token consist?
             * Issuer details- UserApi
             * What is the validity duration of the token?
             * Ex: 30 Minutes
             * What is the scope/audience for this token? i.e. using this token, which of the apps can access the api?
             * To which user?- user email or user name.
             */
        
    }
}
