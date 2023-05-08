using ApiCubosPractica.Helpers;
using ApiCubosPractica.Models;
using ApiCubosPractica.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCubosPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private RepositoryCubos repo;
        private HelperOAuthToken helper;

        public AuthController(RepositoryCubos repo,
            HelperOAuthToken helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        //NECESITAMOS UN METODO PARA VALIDAR A NUESTRO USUARIO
        //Y DEVOLVER EL TOKEN DE ACCESO
        //DICHO METODO SIEMPRE DEBE SER POST
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            Usuarios usuario =
                await this.repo.ExisteUsuario
                ( model.UserName, model.Password);
            if (usuario == null)
            {
                return Unauthorized();
            }
            else
            {
               
                SigningCredentials credentials =
                    new SigningCredentials(this.helper.GetKeyToken()
                    , SecurityAlgorithms.HmacSha256);



                string jsonEmpleado =
                    JsonConvert.SerializeObject(usuario);
                Claim[] informacion = new[]
                {
                    new Claim("UserData",jsonEmpleado)
                };

                JwtSecurityToken token =
                    new JwtSecurityToken(
                        claims: informacion,
                        issuer: this.helper.Issuer,
                        audience: this.helper.Audience,
                        signingCredentials: credentials,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        notBefore: DateTime.UtcNow
                        );
                return Ok(new
                {
                    response =
                    new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
        }

    }
}
