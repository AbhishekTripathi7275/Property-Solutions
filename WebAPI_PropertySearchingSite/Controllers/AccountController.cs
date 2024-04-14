using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI_PropertySearchingSite.Dtos;
using WebAPI_PropertySearchingSite.Interfaces;
using WebAPI_PropertySearchingSite.Models;

namespace WebAPI_PropertySearchingSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration _config;

        public AccountController(IUnitOfWork uow, IConfiguration config)
        {
            this.uow = uow;
            _config = config;
        }

        //api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await uow.UserRepository.Authenticate(loginReq.UserName, loginReq.Password);
            if (user == null)
            {
                return Unauthorized("Invalid User Id or Password");
            }
            var loginRes = new LoginResDto();
            loginRes.UserName = user.UserName;
            loginRes.Token = GenerateToken(user);
            return Ok(loginRes);
        }

        //api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            if (await uow.UserRepository.UserAlreadyExists(loginReq.UserName))
            {
                return BadRequest("User already exists, please try something else");
            }
            uow.UserRepository.Register(loginReq.UserName, loginReq.Password);
            await uow.SaveAsync();
            return StatusCode(201);
        }


        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            var token = new JwtSecurityToken(_config["JWT:ValidIssuer"],
                _config["JWT:ValidAudience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
