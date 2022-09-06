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

        [HttpGet ("nombreUsuario")]

        //end point para traer los datos completos del Usuario buscandolo por el campo nombreUsuario
        public Usuario TraerUsuario(string nombreUsuario)
        {
            return UsuarioHandler.GetUsuarios(nombreUsuario);
            
        }

        [HttpGet("nombreUsuario/contraseña")]

        //end point para utilizar como Login. 
        public string IncioDeSesion (string nombreUsuario, string contraseña)
        {
            //Obtengo un objeto Usuario y compruebo si esta vacio o no
            Usuario usuario = UsuarioHandler.VerificaUsuario(nombreUsuario, contraseña);
            if (usuario.NombreUsuario==null)
            {
                return "Usuario y contraseña incorrectos";
               
            }
            {
                return "Ingreso exitoso";
            }

        }

        [HttpDelete]

        //end point para dar de baja Usuario de la base de datos
        public bool EliminarUsuario([FromBody] int Id)
        {
            return UsuarioHandler.BajaUsuario(Id);
        }

        [HttpPut]
        
        //end point para las modificaciones de datos de Usuario de la base de datos
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

        //end point para dar de alta Usuarios a la base de datos
        public bool CrearUsuario([FromBody]PostUsuario usuario)
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
