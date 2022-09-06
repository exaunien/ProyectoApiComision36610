using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Model;
using MiPrimeraApi.Repository;
using MiPrimeraApi.Controllers.DTOS;



namespace MiPrimeraApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    //Me permite devolver el nombre de mi aplicacion en la api
    public class NombreController : ControllerBase 
    {
        [HttpGet]
       
        //metodo que devuelve el monbre de la aplicacion
        public string TraerNombre()
        {
            return "Proyecto Final MiPrimeraApi";
        }

    }
}
