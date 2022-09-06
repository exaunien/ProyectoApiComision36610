using MiPrimeraApi.Model;
using System.Data.SqlClient;

namespace MiPrimeraApi.Repository
{
    public static class ProductoHandler
    {
        //cadena de conexion a la base de datos
        public const string CadenaConn = "Server=DESKTOP-86FO44B;Initial Catalog=SistemaGestion;" +
            "Trusted_Connection=true";

        //metodo para traer todos los productos
        public static List<Producto> GetProductos()
        {
            string querycommand = "SELECT pd.Id, pd.Descripciones, pd.Costo, pd.PrecioVenta, pd.Stock, " +
                "  us.NombreUsuario " +
                " FROM Producto as pd INNER JOIN Usuario AS us ON pd.IdUsuario = us.Id; ";


            List<Producto> resultado = new List<Producto>();

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
                                    Producto producto = new Producto();
                                    producto.Id = Convert.ToInt32(lector["Id"]);
                                    producto.Descripciones = lector["Descripciones"].ToString();
                                    producto.Costo = Convert.ToInt32(lector["Costo"]);
                                    producto.PrecioVenta = Convert.ToInt32(lector["PrecioVenta"]);
                                    producto.Stock = Convert.ToInt32(lector["Stock"]);
                                    producto.NombreUsuario = (lector["NombreUsuario"].ToString());
                                    resultado.Add(producto);
                                }
                            }
                        }

                    }
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return resultado;

        }

        //metodo para dar de baja un producto
        public static bool BajaProducto(int id)
        {
            bool resultado = false;

            try //comandos a la base de datos
            {
                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    //Consulta a la base de datos
                    string queryCommand = "DELETE FROM Producto WHERE Id = @idProducto;";

                    conn.Open();

                    //se crean los parametros necesarios como para ejecutar la consulta a la base de datos
                    SqlParameter parametro = new SqlParameter("idProducto", System.Data.SqlDbType.BigInt);
                    parametro.Value = id;

                    using (SqlCommand sqlcomm = new SqlCommand(queryCommand, conn))
                    {
                        sqlcomm.Parameters.Add(parametro);
                        int filas = sqlcomm.ExecuteNonQuery();//Se realiza la baja del producto
                        if (filas > 0)
                        {
                            resultado = true; //se dio de baja con exito
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

        //Metodo para las altas de productos
        public static bool AltaProducto(Producto producto)
        {
            bool resultado = false;

            try // Comandos para la base de datos
            {

                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    //Consulta a la base de datos
                    string queryCommand = "INSERT INTO Producto(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES(@descripciones, @costo, @precioVenta, @stock, @idUsuario);";

                    //se crean los parametros necesarios como para ejecutar la consulta a la base de datos
                    SqlParameter parametroDescripciones = new SqlParameter("descripciones", System.Data.SqlDbType.VarChar);
                    parametroDescripciones.Value = producto.Descripciones;
                    SqlParameter parametroCosto = new SqlParameter("costo", System.Data.SqlDbType.VarChar);
                    parametroCosto.Value = producto.Costo;
                    SqlParameter parametroPrecioVenta = new SqlParameter("precioVenta", System.Data.SqlDbType.VarChar);
                    parametroPrecioVenta.Value = producto.PrecioVenta;
                    SqlParameter parametroStock = new SqlParameter("stock", System.Data.SqlDbType.VarChar);
                    parametroStock.Value = producto.Stock;
                    SqlParameter parametroIdUsuario = new SqlParameter("idUsuario", System.Data.SqlDbType.VarChar);
                    parametroIdUsuario.Value = producto.IdUsuario;

                    conn.Open();
                    using (SqlCommand sqlcomm = new SqlCommand(queryCommand, conn))
                    {
                        sqlcomm.Parameters.Add(parametroDescripciones);
                        sqlcomm.Parameters.Add(parametroCosto);
                        sqlcomm.Parameters.Add(parametroPrecioVenta);
                        sqlcomm.Parameters.Add(parametroStock);
                        sqlcomm.Parameters.Add(parametroIdUsuario);

                        int filas = sqlcomm.ExecuteNonQuery();//se crea el alta del producto a la base de datos
                        if (filas > 0)
                        {
                            return true;//Producto creado
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

        //metodo para la modificacion de los productos en la base de datos
        public static bool ModificarProducto(Producto producto)
        {
            bool resultado = false;

            try //Comandos a la base de datos
            {

                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    //Consulta a la base de datos
                    string queryCommand = "UPDATE Producto SET Descripciones=@descripciones, Costo=@costo, PrecioVenta=@precioVenta, Stock=@stock, IdUsuario=@idUsuario WHERE Id=@id;";

                    //se crean los parametros necesarios como para ejecutar la consulta a la base de datos
                    SqlParameter parametroId = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                    parametroId.Value = producto.Id;
                    SqlParameter parametroDescripciones = new SqlParameter("descripciones", System.Data.SqlDbType.VarChar);
                    parametroDescripciones.Value = producto.Descripciones;
                    SqlParameter parametroCosto = new SqlParameter("costo", System.Data.SqlDbType.VarChar);
                    parametroCosto.Value = producto.Costo;
                    SqlParameter parametroPrecioVenta = new SqlParameter("precioVenta", System.Data.SqlDbType.VarChar);
                    parametroPrecioVenta.Value = producto.PrecioVenta;
                    SqlParameter parametroStock = new SqlParameter("stock", System.Data.SqlDbType.VarChar);
                    parametroStock.Value = producto.Stock;
                    SqlParameter parametroIdUsuario = new SqlParameter("idUsuario", System.Data.SqlDbType.VarChar);
                    parametroIdUsuario.Value = producto.IdUsuario;

                    conn.Open();
                    using (SqlCommand sqlcomm = new SqlCommand(queryCommand,conn))
                    {
                        sqlcomm.Parameters.Add(parametroId);
                        sqlcomm.Parameters.Add(parametroDescripciones);
                        sqlcomm.Parameters.Add(parametroCosto);
                        sqlcomm.Parameters.Add(parametroPrecioVenta);
                        sqlcomm.Parameters.Add(parametroStock);
                        sqlcomm.Parameters.Add(parametroIdUsuario);

                        int filas = sqlcomm.ExecuteNonQuery();//se realiza la modificacion del producto
                        if (filas > 0)
                        {
                            resultado = true;//modoficacion realizada
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

