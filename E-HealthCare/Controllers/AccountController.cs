using E_HealthCare.Dtos;
using E_HealthCare.Interfaces;
using E_HealthCare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_HealthCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        private readonly IConfiguration configuration;
        public AccountController(IUnitOfWork uow,IConfiguration configuration)
        {
            this.configuration = configuration;
            this.uow = uow;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var userinfo = await uow.UserInfoRepository.Authenticate(loginReq.UserName, loginReq.Password);

            if (userinfo == null)
            {
                return Unauthorized("Invalid user id or password");
            }
            var loginRes = new LoginResDto();
            loginRes.UserName = userinfo.Username;
            loginRes.Token = CreateJWT(userinfo);
            return Ok(loginRes); 
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            if (await uow.UserInfoRepository.UseralreadyExists(loginReq.UserName))
                return BadRequest("User already exists");
            uow.UserInfoRepository.Register(loginReq.UserName, loginReq.Password);
            await uow.SaveAsync();
            return StatusCode(201);
        }

            private string CreateJWT(UserInfo userinfo)
        {

            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(secretKey));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,userinfo.Username),
                new Claim(ClaimTypes.NameIdentifier,userinfo.Id.ToString())
            };

            var signingCredentials = new SigningCredentials(
                  key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}