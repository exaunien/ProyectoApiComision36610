namespace MiPrimeraApi.Model
{
    public class ProductoVendido : Producto
    {
        public new int Id { get; set; }
        public new int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }
    }
}
