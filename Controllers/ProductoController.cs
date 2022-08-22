using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Model;
using MiPrimeraApi.Repository;
using MiPrimeraApi.Controllers.DTOS;

namespace MiPrimeraApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet (Name ="GetProductos")]
        public List<Producto> GetProductos()
        {
            return ProductoHandler.GetProductos();
        }

        [HttpDelete]
        public bool BajaProducto([FromBody] int Id)
        {
            return ProductoHandler.BajaProducto(Id);
        }

        [HttpPost]
        public bool AltaProducto([FromBody] PostProductos producto )
        {
            return ProductoHandler.AltaProducto(new Producto
            {
                Descripciones=producto.Descripciones,
                Costo=producto.Costo,
                PrecioVenta=producto.PrecioVenta,
                Stock=producto.Stock,
                IdUsuario=producto.IdUsuario,


            });


        }

        [HttpPut]

        public bool ModificarProducto([FromBody] PutProducto producto)
        {
            return ProductoHandler.ModificarProducto(new Producto
            {
                Id = producto.Id,
                Descripciones = producto.Descripciones,
                Costo = producto.Costo,
                PrecioVenta = producto.PrecioVenta,
                Stock = producto.Stock,
                IdUsuario = producto.IdUsuario


            }) ; 


        }



    }
}
