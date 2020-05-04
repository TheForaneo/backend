using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;  
using MongoDB.Bson;  
using webapi.Models;
using webapi.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace webapi.Controllers{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller{
        private readonly IConfiguration _configuration;
        private readonly ClienteService _clienteService;
        public AuthController(IConfiguration configuration, ClienteService clienteService){
            _configuration = configuration;
            _clienteService = clienteService;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult Login(UserLogin obj){
            
            var user = _clienteService.iniciaSesion(obj);

            if(user != null){
                var SecretKey = _configuration.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(SecretKey);

                var tokenDescriptor = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(new Claim[]{
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.correo)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokerHandler = new JwtSecurityTokenHandler();
                var createdToken = tokerHandler.CreateToken(tokenDescriptor);
                return Content(createdToken.ToString());
            }
            return NoContent();
        }
    }
}