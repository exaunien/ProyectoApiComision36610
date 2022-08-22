namespace MiPrimeraApi.Controllers.DTOS
{
    public class PostProductos
    {
        public string Descripciones { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }

        public int IdUsuario { get; set; }
    }
}
