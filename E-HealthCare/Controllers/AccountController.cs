using E_HealthCare.Dtos;
using E_HealthCare.Interfaces;
using E_HealthCare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public AccountController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserInfo userinfo)
        {
            var user = await uow.UserInfoRepository.Authenticate(userinfo.Username, userinfo.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            var loginRes = new LoginResDto();
            loginRes.UserName = user.Username;
            loginRes.Token = CreateJWT(user);
            return Ok(loginRes); 
        }

        private string CreateJWT(UserInfo userinfo)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes("this is a secret"));
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
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}