using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Model;
using MiPrimeraApi.Repository;

namespace MiPrimeraApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpGet(Name ="TraerVentas")]
        //End point Traer Venta
        public List<Venta> TraerVentas()
        {
            return VentaHandler.TraerVentas();
        }

        [HttpPost("{idUsuario}")]
        public bool CargarVenta(List<Producto> productos, int idUsuario)
        {
          return  VentaHandler.CargarVentas(productos, idUsuario);
        }

        [HttpDelete]
        // end point para borrar productos de la base de datos
        public bool EliminarVenta([FromBody] int Id)
        {
            return VentaHandler.BajaVenta(Id);
        }

    }
}
