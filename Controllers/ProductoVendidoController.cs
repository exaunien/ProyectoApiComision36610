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
        [HttpGet(Name ="GetProductosVendidos")]
        public List<ProductoVendido> GetProductosVendidos()
        {
            return ProductoVendidoHandler.GetProductosVendidos();
        }

        [HttpDelete]
        public bool BajaProductoVendido([FromBody] int Id)
        {
            return ProductoVendidoHandler.BajaProductoVendido(Id);
        }

        [HttpPost]
        public bool AltaProductoVendido([FromBody] PostProductoVendido venta)
        {
            return ProductoVendidoHandler.AltaProductoVendido(new ProductoVendido
            {
                Stock = venta.Stock,
                IdProducto = venta.IdProducto,
                IdVenta = venta.IdVenta


            });


        }

        [HttpPut]

        public bool ModificacionProductoVendido([FromBody] PutProductoVendido venta)
        {
            return ProductoVendidoHandler.ModificacionProductoVendido(new ProductoVendido
            {
                Id=venta.Id,
                Stock=venta.Stock,
                IdProducto=venta.IdProducto,
                IdVenta=venta.IdVenta

            });


        }

    }
}
