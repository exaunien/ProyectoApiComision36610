using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Model;
using MiPrimeraApi.Repository;
using MiPrimeraApi.Controllers.DTOS;

namespace MiPrimeraApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet("nombreUsuario")]
        //end point para listar productos vendidos por un usuario
        public List<ProductoVendido> TraerProductosVendidos(string nombreUsuario)
        {
            return ProductoVendidoHandler.GetProductosVendidos(nombreUsuario);
        }

    }
}
