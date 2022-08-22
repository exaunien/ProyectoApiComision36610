using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Controllers.DTOS;
using MiPrimeraApi.Model;
using MiPrimeraApi.Repository;

namespace MiPrimeraApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
       

        [HttpGet (Name ="GetUsuarios")]
        public List<Usuario> GetUsuarios()
        {
            return UsuarioHandler.GetUsuarios();
            
        }

        [HttpGet("nombreUsuario/contraseña")]
        public bool ExisteUsuario (string nombreUsuario, string contraseña)
        {
            Usuario usuario = UsuarioHandler.VerificaUsuario(nombreUsuario, contraseña);
            if (usuario.NombreUsuario==null)
            {
                return false;
            }
            {
                return true;
            }

        }

        [HttpDelete]
        public bool BajasUsuario([FromBody] int Id)
        {
            return UsuarioHandler.BajaUsuario(Id);
        }

        [HttpPut]
        public bool ModificarUsuario([FromBody]PutUsuario usuario)
        {
            return UsuarioHandler.ModificarUsuario(new Usuario
            {
                Id = usuario.Id,
                Nombre=usuario.Nombre,
                Apellido=usuario.Apellido,
                NombreUsuario=usuario.NombreUsuario,
                Contraseña=usuario.Contraseña,
                Mail=usuario.Mail

            });

        }

        [HttpPost]

        public bool AltasUsuario([FromBody]PostUsuario usuario)
        {
            return UsuarioHandler.AltaUsuario(new Usuario
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Contraseña = usuario.Contraseña,
                Mail = usuario.Mail

            });

               
        }

        
        
       

    }
}
