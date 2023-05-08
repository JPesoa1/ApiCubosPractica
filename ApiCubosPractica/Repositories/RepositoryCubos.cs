using ApiCubosPractica.Data;
using ApiCubosPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCubosPractica.Repositories
{
    public class RepositoryCubos
    {
        private CubosContext context;

        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }

        private int MaxIdUsuario() 
        {
            return this.context.Usuarios.Max(x => x.IdUsuario) + 1;
        }

        private int MaxIdCubo()
        {
            return this.context.Cubos.Max(x => x.IdCubo) + 1;
        }


        private int MaxIdCompra()
        {
            return this.context.Compras.Max(x => x.Idpedido) + 1;
        }

        public async Task<Usuarios> ExisteUsuario(string email, string pass)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(x => x.Email==email && x.Pass == pass);
        }
        public async Task<List<Cubos>> GetCubosAsync()
            => await this.context.Cubos.ToListAsync();

        public async Task<List<Cubos>> GetCubosMarcaAsync(string marca)
            => await this.context.Cubos.Where(x => x.Marca == marca).ToListAsync();
        public async Task InsertarUsuarioAsync
            (string nombre,string email,string pass,string? imagen)
        {
            Usuarios usuarios = new Usuarios();
            usuarios.IdUsuario = this.MaxIdUsuario();
            usuarios.Nombre = nombre;
            usuarios.Email = email;
            usuarios.Pass = pass;
            usuarios.Imagen = imagen;

            await this.context.Usuarios.AddAsync(usuarios);
            await this.context.SaveChangesAsync();
        }

        public async Task InsertarCuboAsync
            (string nombre,string marca,string? imagen, int precio)
        {
            Cubos cubos = new Cubos();
            cubos.IdCubo = this.MaxIdCubo();
            cubos.Nombre = nombre;
            cubos.Marca = marca;
            cubos.Imagen = imagen;
            cubos.Precio = precio;

            await this.context.Cubos.AddAsync(cubos);
            await this.context.SaveChangesAsync();
        }


        public async Task<Usuarios> FindUsuarioAsync(int idusuario)
        {
            return await this.context.Usuarios
                .FirstOrDefaultAsync(x =>x.IdUsuario==idusuario);
        }

        public async Task<List<Compra>> GetComprasAsync(int idusuario)
        {
            return await this.context.Compras.Where(x => x.IdUsuario == idusuario).ToListAsync();
        }

        public async Task InsertarCompraAsync
            (int idcubo,int idusuario,DateTime fecha)

        {
            Compra compra = new Compra();
            compra.Idpedido = this.MaxIdCompra();
            compra.IdCubo = idcubo;
            compra.IdUsuario = idusuario;
            compra.Fecha = fecha;

            await this.context.Compras.AddAsync(compra);
            await this.context.SaveChangesAsync();
            
        }
    }
}
