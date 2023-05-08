using ApiCubosPractica.Models;
using ApiCubosPractica.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiCubosPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubosController : ControllerBase
    {
        private RepositoryCubos repo;

        public CubosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Cubos>>> GetCubos()
        {
            return await this.repo.GetCubosAsync();
        }

        [HttpGet]
        [Route("[action]/{marca}")]

        public async Task<ActionResult<List<Cubos>>> GetCuboMarca(string marca)
        {
            return await this.repo.GetCubosMarcaAsync(marca);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> InsertarUsuario(Usuarios usuarios)
        {
            await this.repo.InsertarUsuarioAsync(usuarios.Nombre,usuarios.Email,usuarios.Pass,usuarios.Imagen);
            return Ok();
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> InsertarCubo(Cubos cubos)
        {
            await this.repo.InsertarCuboAsync
                (cubos.Nombre,cubos.Marca,cubos.Imagen,cubos.Precio);
            return Ok();
        }


        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult<Usuarios>> PerfilUsuario()
        {
            Claim claim = HttpContext.User.Claims
                .SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario = claim.Value;

            Usuarios usuarios = JsonConvert.DeserializeObject<Usuarios>
                (jsonUsuario);

            return usuarios;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult<List<Compra>>> Pedidos()
        {
            Claim claim = HttpContext.User.Claims
                .SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario = claim.Value;

            Usuarios usuarios = JsonConvert.DeserializeObject<Usuarios>
                (jsonUsuario);
            return await this.repo.GetComprasAsync(usuarios.IdUsuario);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult> InsertarPedido(Compra compra)
        {

            await this.repo.InsertarCompraAsync(compra.IdCubo,compra.IdUsuario,compra.Fecha);
            return Ok();
        }




    }
}
