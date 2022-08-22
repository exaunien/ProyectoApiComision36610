using MiPrimeraApi.Model;
using System.Data.SqlClient;

namespace MiPrimeraApi.Repository
{
    public static class ProductoVendidoHandler
    {
        public const string CadenaConn = "Server=DESKTOP-86FO44B;Initial Catalog=SistemaGestion;" +
            "Trusted_Connection=true";
       
        public static List<ProductoVendido> GetProductosVendidos()
        {
            const string querycommand = "SELECT pv.Id, pv.stock, pd.Descripciones, us.NombreUsuario FROM ProductoVendido AS pv INNER JOIN Producto AS pd  ON pv.IdProducto = pd.Id INNER JOIN Usuario AS us ON us.id = pv.IdVenta;";


            List<ProductoVendido> resultado = new List<ProductoVendido>();

            try
            {
                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    conn.Open();

                    using (SqlCommand sqlcom = new SqlCommand(querycommand, conn))
                    {
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

        public static bool BajaProductoVendido(int id)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    string queryCommand = "DELETE FROM ProductoVendido WHERE Id = @idProducto;";
                    conn.Open();
                    SqlParameter parametro = new SqlParameter("idProducto", System.Data.SqlDbType.BigInt);
                    parametro.Value = id;
                    using (SqlCommand sqlcomm = new SqlCommand(queryCommand, conn))
                    {
                        sqlcomm.Parameters.Add(parametro);
                        int filas = sqlcomm.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            resultado = true;
                        }

                    }
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultado = false;
            }
            return resultado;
        }

        public static bool AltaProductoVendido(ProductoVendido venta)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    string queryCommand = "INSERT INTO ProductoVendido(Stock, IdProducto, IdVenta) VALUES(@stock, @idProducto, @idVenta);";
                    SqlParameter parametroStock = new SqlParameter("stock", System.Data.SqlDbType.Int);
                    parametroStock.Value = venta.Stock;
                    SqlParameter parametroIdProducto = new SqlParameter("idProducto", System.Data.SqlDbType.Int);
                    parametroIdProducto.Value = venta.IdProducto;
                    SqlParameter parametroIdVenta = new SqlParameter("idVenta", System.Data.SqlDbType.Int);
                    parametroIdVenta.Value = venta.IdVenta;
                    

                    conn.Open();
                    using (SqlCommand sqlcomm = new SqlCommand(queryCommand, conn))
                    {
                        sqlcomm.Parameters.Add(parametroStock);
                        sqlcomm.Parameters.Add(parametroIdProducto);
                        sqlcomm.Parameters.Add(parametroIdVenta);
                       
                        int filas = sqlcomm.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            return true;
                        }



                    }

                    conn.Close();


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }
            return resultado;
        }

        public static bool ModificacionProductoVendido(ProductoVendido venta)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    string queryCommand = "UPDATE ProductoVendido SET Stock = @stock, IdProducto = @idProducto, IdVenta = @idVenta WHERE Id = @id;";

                    SqlParameter parametroId = new SqlParameter("id", System.Data.SqlDbType.Int);
                    parametroId.Value = venta.Id;
                    SqlParameter parametroStock = new SqlParameter("stock", System.Data.SqlDbType.Int);
                    parametroStock.Value = venta.Stock;
                    SqlParameter parametroIdProducto = new SqlParameter("idProducto", System.Data.SqlDbType.Int);
                    parametroIdProducto.Value = venta.IdProducto;
                    SqlParameter parametroIdVenta = new SqlParameter("idVenta", System.Data.SqlDbType.Int);
                    parametroIdVenta.Value = venta.IdVenta;


                    conn.Open();
                    using (SqlCommand sqlcomm = new SqlCommand(queryCommand, conn))
                    {
                        sqlcomm.Parameters.Add(parametroId);
                        sqlcomm.Parameters.Add(parametroStock);
                        sqlcomm.Parameters.Add(parametroIdProducto);
                        sqlcomm.Parameters.Add(parametroIdVenta);

                        int filas = sqlcomm.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            resultado = true;
                        }

                    }

                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }
            return resultado;
        }

    }

}

