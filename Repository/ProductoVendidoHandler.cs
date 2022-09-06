using MiPrimeraApi.Model;
using System.Data.SqlClient;

namespace MiPrimeraApi.Repository
{
    public static class ProductoVendidoHandler
    {
        //Cadena de conexion con la base de datos
        public const string CadenaConn = "Server=DESKTOP-86FO44B;Initial Catalog=SistemaGestion;" +
            "Trusted_Connection=true";
       
        //Metodo para traer los productos vendidos por un usuario
        public static List<ProductoVendido> GetProductosVendidos(string nombreUsuario)
        {
            //consulta a la base de datos
            string querycommand = "SELECT pv.Id, pv.Stock, pd.Descripciones, us.NombreUsuario " +
            " FROM ProductoVendido AS pv INNER JOIN Producto AS pd ON pv.IdProducto = pd.Id " +
            " INNER JOIN Usuario AS us ON us.Id = @id AND pd.IdUsuario = @id; ";

            //determino id del usuario a listar
           
            Usuario user = UsuarioHandler.GetUsuarios(nombreUsuario);
            var id = user.Id;

            List<ProductoVendido> resultado = new List<ProductoVendido>();//Instancia de la lista de productos vendidos

            try // comandos a la base de datos
            {
                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    conn.Open();

                    using (SqlCommand sqlcom = new SqlCommand(querycommand, conn))
                    {
                        //se crean los parametros necesarios como para ejecutar la consulta a la base de datos
                        SqlParameter parametroId = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                        parametroId.Value = id;
                        sqlcom.Parameters.Add(parametroId);

                        using (SqlDataReader lector = sqlcom.ExecuteReader())
                        {
                            if (lector.HasRows)
                            {
                                while (lector.Read())
                                {
                                    ProductoVendido venta = new ProductoVendido();
                                    venta.Id = Convert.ToInt32(lector["Id"]);
                                    venta.Stock = Convert.ToInt32(lector["Stock"]);
                                    venta.NombreUsuario = (lector["NombreUsuario"].ToString());
                                    venta.Descripciones = (lector["Descripciones"].ToString());
                                    resultado.Add(venta);
                                }
                            }
                        }

                    }
                    conn.Close();

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            return resultado;

        }

        
    }

}

