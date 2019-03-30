using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DAO.API;
using API.Models.API;
using API.Models.ViewModel;
using API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration Configuration { get; set; }
        private ConexaoGeral ConexaoGeral { get; set; }

        public TokenController(IConfiguration configuration, ConexaoGeral conexao)
        {
            Configuration = configuration;
            ConexaoGeral = conexao;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetToken([FromBody] LoginApiViewModel user)
        {
            var usuario = new Usuario();

            if (!usuario.Login(user.Usuario, user.Senha, ConexaoGeral, null))
                return BadRequest("Usuário e/ou senha inválidos.");

            var symmetricKey = Convert.FromBase64String(Configuration["SecurityKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            var subject = new ClaimsIdentity();
            subject.AddClaim(new Claim(ClaimTypes.Name, user.Usuario));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                //Audience = _configuration["Audience"],
                Issuer = Configuration["Issuer"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(tokenHandler.WriteToken(stoken));
        }
    }
}