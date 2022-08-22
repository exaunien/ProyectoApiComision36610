namespace MiPrimeraApi.Controllers.DTOS
{
    public class PostProductoVendido
    {
       
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }
    }
}
