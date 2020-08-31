using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RouletteAPI.Models;
using RouletteAPI.Models.Login.Request;
using RouletteAPI.Models.Login.Response;
using RouletteAPI.Services.Contracts;

namespace RouletteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        #region Properties
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        #endregion
        #region Constructor
        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }
        #endregion

        #region Methods
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse<LoginResponse>>> Login(LoginRequest request)
        {
            BaseResponse<PersonResponse> resp = await _authService.Login(request);
            if (!string.IsNullOrEmpty(resp.message))
                return BadRequest(new BaseResponse<LoginResponse> {Reponse=null, message=resp.message });
            LoginResponse response = new LoginResponse { Token = GeneratedToken(resp.Reponse) };
            return Ok(new BaseResponse<LoginResponse> { Reponse = response });
        }

        #region privateMethods
        private string GeneratedToken(PersonResponse person)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);
            var Claims = new List<Claim> {
                new Claim("idPerson", person.id.ToString()),
                new Claim("name", person.Name),
                new Claim("user", person.User)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT:Expiration"])),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
        #endregion
    }
}
