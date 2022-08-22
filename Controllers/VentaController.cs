using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Model;
using MiPrimeraApi.Repository;

namespace MiPrimeraApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpGet(Name ="GetVentas")]
        public List<Venta> GetVentas()
        {
            return VentaHandler.GetVentas();
        }
    }
}
