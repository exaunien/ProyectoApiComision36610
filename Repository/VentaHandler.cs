using MiPrimeraApi.Model;
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace MiPrimeraApi.Repository
{
    public static class VentaHandler
    {
        //Cadena de conexion a la base de datos
        public const string CadenaConn = "Server=DESKTOP-86FO44B;Initial Catalog=SistemaGestion;" +
            "Trusted_Connection=true";

        //Metodo para listar las ventas
        public static  List<Venta> TraerVentas()
        {
            string querycommand = " SELECT vta.Id, vta.Comentarios, pvdo.Stock, pcto.Descripciones " +
                                  " FROM Venta AS vta INNER JOIN ProductoVendido AS pvdo ON vta.Id = pvdo.IdVenta " +
                        " INNER JOIN Producto AS pcto ON pcto.Id = pvdo.IdProducto ; ";

            List<Venta> resultado = new List<Venta>();

            try
            {

                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    conn.Open();

                    SqlCommand sqlcomm = new SqlCommand(querycommand, conn);

                    SqlDataReader lector = sqlcomm.ExecuteReader();

                    if (lector.HasRows)
                    {
                        while (lector.Read())
                        {
                            Venta ventas = new Venta();
                            ventas.Id = Convert.ToInt32(lector["Id"]);
                            ventas.Comentarios = lector["Comentarios"].ToString();
                            ventas.Stock = Convert.ToInt32(lector["Stock"]);
                            ventas.Descripciones = lector["Descripciones"].ToString();

                            resultado.Add(ventas);
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }

        //Metodo para cargar ventas a la base de datos
        public static bool CargarVentas(List<Producto> productos, int IdUsuario)
        {
            Venta venta = new Venta();
            bool resultado = false;

            try
            {
                string queryCommand = "INSERT INTO Venta (Comentarios) VALUES (@Comentarios);";
                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    conn.Open();
                    SqlParameter parametroComentarios = new SqlParameter("Comentarios", System.Data.SqlDbType.VarChar);
                    parametroComentarios.Value = "Venta hecha : " + DateTime.Now.ToString("dd-MM-yyyy");

                    SqlCommand sqlcom = new SqlCommand();
                    sqlcom.Connection = conn;
                    sqlcom.CommandText = queryCommand;

                    sqlcom.Parameters.Add(parametroComentarios);
                    sqlcom.ExecuteNonQuery();
                    sqlcom.Parameters.Clear();

                    venta.Id = IdNuevo(sqlcom);
                   
                    foreach (Producto producto in productos)
                    {
                        queryCommand = " INSERT INTO ProductoVendido([Stock],[IdProducto],[IdVenta]) " +
                        " VALUES(@Stock,@IdProducto,@IdVenta);";
                        SqlParameter parametroStock = new SqlParameter("Stock", System.Data.SqlDbType.BigInt);
                        parametroStock.Value = producto.Stock;
                        SqlParameter parametroIdProducto = new SqlParameter("IdProducto", System.Data.SqlDbType.BigInt);
                        parametroIdProducto.Value = producto.Id;
                        SqlParameter parametroIdVenta = new SqlParameter("IdVenta", System.Data.SqlDbType.BigInt);
                        parametroIdVenta.Value = venta.Id;
                        
                        sqlcom.CommandText = queryCommand;

                        sqlcom.Parameters.Add(parametroStock);
                        sqlcom.Parameters.Add(parametroIdProducto);
                        sqlcom.Parameters.Add(parametroIdVenta);

                        sqlcom.ExecuteNonQuery(); 
                        sqlcom.Parameters.Clear();

                        queryCommand = "UPDATE Producto SET Stock = Stock - @Stock WHERE id = @IdProducto;";
                        parametroStock.Value = producto.Stock;
                        parametroIdProducto.Value = producto.Id;

                        sqlcom.CommandText = queryCommand;

                        sqlcom.Parameters.Add(parametroStock);
                        sqlcom.Parameters.Add(parametroIdProducto);

                        sqlcom.ExecuteNonQuery(); 
                        sqlcom.Parameters.Clear();
                        resultado = true;
                                               
                    }
                    
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            return resultado;

        }

        //Metodo que devuelve el ultimo indice generado en tabla ventas
        public static int IdNuevo(SqlCommand sqlcomm)
        {
            sqlcomm.CommandText = "Select @@IDENTITY";
            object UltimoId = sqlcomm.ExecuteScalar();//obtengo el ultimo id creado
            int id = Convert.ToInt32(UltimoId);
            return id; //se retorna para darle valor al parametro que participa en las demas consultas
        }

        //metodo para dar de baja una venta por su Id
        public static bool BajaVenta(int id)
        {
            bool resultado = false;

            try //comandos a la base de datos
            {
                // instancio la lista que se a llenar con los productos vendidos en la venta a borrar
                List<ProductoVendido> listaProductosVendidos = new List<ProductoVendido>();

                //Consulta a la base de datos
                string queryCommand = "SELECT * FROM ProductoVendido WHERE IdVenta = @id;";

                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    
                    SqlParameter parametroIdVenta = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                    parametroIdVenta.Value = id;
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = conn;
                    sqlcomm.CommandText = queryCommand;
                    sqlcomm.Connection.Open();
                    sqlcomm.Parameters.Add(parametroIdVenta);
                    SqlDataReader lector = sqlcomm.ExecuteReader();

                    if (lector.HasRows)
                    {
                        resultado = true;
                        while (lector.Read())
                        {
                            ProductoVendido productoVendido = new ProductoVendido();
                            productoVendido.Id = Convert.ToInt32(lector["Id"]);
                            productoVendido.Stock = Convert.ToInt32(lector["Stock"]);
                            productoVendido.IdProducto = Convert.ToInt32(lector["IdProducto"]);
                            productoVendido.IdVenta = Convert.ToInt32(lector["IdVenta"]);
                            listaProductosVendidos.Add(productoVendido);

                        }
                        sqlcomm.Connection.Close();
                    }
                   
                    foreach(ProductoVendido vendidos in listaProductosVendidos)
                    {
                        queryCommand = "UPDATE Producto SET Stock = Stock + @stock WHERE Id=@IdProducto;";
                        SqlParameter parametroStock = new SqlParameter("stock", System.Data.SqlDbType.Int);
                        parametroStock.Value = vendidos.Stock;
                        SqlParameter parametroIdProducto = new SqlParameter("IdProducto", System.Data.SqlDbType.BigInt);
                        parametroIdProducto.Value = vendidos.IdProducto;
                        sqlcomm.Connection = conn;
                        sqlcomm.Connection.Open();
                        sqlcomm.Parameters.Add(parametroStock);
                        sqlcomm.Parameters.Add(parametroIdProducto);
                        sqlcomm.CommandText=queryCommand;
                        sqlcomm.ExecuteNonQuery();//devuelvo la venta al stock producto
                        sqlcomm.Parameters.Clear();

                        queryCommand = "DELETE ProductoVendido WHERE IdVenta = @id;";
                        sqlcomm.Parameters.Add(parametroIdVenta);
                        sqlcomm.CommandText = queryCommand;
                        sqlcomm.ExecuteNonQuery();//se da de baja el producto vendido
                        sqlcomm.Parameters.Clear();
                    }
                    queryCommand = "DELETE Venta WHERE Id = @id;";
                    sqlcomm.Parameters.Add(parametroIdVenta);
                    sqlcomm.CommandText = queryCommand;
                    sqlcomm.ExecuteNonQuery();//se da de baja la venta
                    sqlcomm.Parameters.Clear();
                    sqlcomm.Connection.Close();
                    

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultado = false;
            }
            return resultado;
        }

        
    }
}

    
