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
        [HttpGet (Name ="TraerProductos")]
        //end point para traer los productos
        public List<Producto> TraerProductos()
        {
            return ProductoHandler.GetProductos();
        }

        [HttpDelete]
        // end point para borrar productos de la base de datos
        public bool EliminarProducto([FromBody] int Id)
        {
            return ProductoHandler.BajaProducto(Id);
        }

        [HttpPost]
        //end point para crear producto
        public bool CrearProducto([FromBody] PostProductos producto )
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
        //end point para modificar los datos existentes de un producto
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
