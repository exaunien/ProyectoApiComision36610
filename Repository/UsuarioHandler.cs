using MiPrimeraApi.Model;
using System.Data.SqlClient;

namespace MiPrimeraApi.Repository
{
    public static class UsuarioHandler
    {
        //Cadena de conexion a la base de datos
        public const string CadenaConn = "Server=DESKTOP-86FO44B;Initial Catalog=SistemaGestion;" +
            "Trusted_Connection=true";
        
        //Metodo para traer usuario por su nombre
        public static Usuario GetUsuarios(string nombreUsuario)
        {
            //Consulta a la base de datos
            string querycommand = "SELECT * FROM Usuario WHERE NombreUsuario=@nombreUsuario";

            Usuario resultado = new Usuario();

            try //Comandos de consultas a la base de datos
            {
                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    conn.Open();

                    using (SqlCommand sqlcom = new SqlCommand(querycommand, conn))
                    {
                        //se crean los parametros necesarios como para ejecutar la consulta a la base de datos
                        SqlParameter parametro = new SqlParameter("nombreUsuario", System.Data.SqlDbType.VarChar);
                        parametro.Value = nombreUsuario;
                        sqlcom.Parameters.Add(parametro);

                        using (SqlDataReader lector = sqlcom.ExecuteReader()) //Se trae al usuario
                        {
                            if (lector.HasRows)
                            {
                                while (lector.Read()) //Se leen sus datos
                                {
                                    Usuario usuario = new Usuario();
                                    usuario.Id = Convert.ToInt32(lector["Id"]);
                                    usuario.Nombre = lector["Nombre"].ToString();
                                    usuario.Apellido = lector["Apellido"].ToString();
                                    usuario.NombreUsuario = lector["NombreUsuario"].ToString();
                                    usuario.Contraseña = lector["Contraseña"].ToString();
                                    usuario.Mail = lector["Mail"].ToString();
                                    resultado=usuario;
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

        //Metodo que devuelve un objeto Usuario con los parametros NombreUsuario y Contraseña
        public static Usuario VerificaUsuario(string nombreUsuario, string contraseña)
        {
            //consulta a la base de datos
            string queryCommand = "SELECT * FROM Usuario WHERE Usuario.NombreUsuario = @nombreUsuario AND Usuario.Contraseña = @contraseña;";

            Usuario resultado = new Usuario();  

            try //comandos de consultas a la base de datos
            {
                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    //se crean los parametros necesarios como para ejecutar la consulta a la base de datos
                    SqlParameter parametroUser = new SqlParameter("nombreUsuario", System.Data.SqlDbType.VarChar);
                    parametroUser.Value = nombreUsuario;
                    SqlParameter parametroContraseña = new SqlParameter("contraseña", System.Data.SqlDbType.VarChar);
                    parametroContraseña.Value = contraseña;
                    
                    conn.Open();

                    using (SqlCommand sqlcomm = new SqlCommand(queryCommand, conn))
                    {
                        sqlcomm.Parameters.Add(parametroUser);
                        sqlcomm.Parameters.Add(parametroContraseña);
                       using (SqlDataReader lector = sqlcomm.ExecuteReader()) //devuelve resultado de la busqueda
                        {
                            if (lector.HasRows)
                            {
                                while (lector.Read()) //Se leen sus datos
                                {
                                    Usuario usuario = new Usuario(); 
                                    usuario.Id = Convert.ToInt32(lector["Id"]);
                                    usuario.Nombre = lector["Nombre"].ToString();
                                    usuario.Apellido = lector["Apellido"].ToString();
                                    usuario.NombreUsuario = lector["NombreUsuario"].ToString();
                                    usuario.Contraseña = lector["Contraseña"].ToString();
                                    usuario.Mail = lector["Mail"].ToString();
                                    resultado = usuario;


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
            //Devuelve un objeto vacio (si no existen los parametros enviados a la base de datos)
            return resultado;
        }

        //Metodo para las bajas de usuarios por Id
        public static bool BajaUsuario (int id)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    string queryCommand = "DELETE FROM Usuario WHERE Id = @idUsuario;";
                    conn.Open();

                    //se crean los parametros necesarios como para ejecutar la consulta a la base de datos
                    SqlParameter parametro = new SqlParameter("idUsuario", System.Data.SqlDbType.BigInt);
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

        //Metodo para dar de alta un usuario en la base de datos
        public static bool AltaUsuario(Usuario usuario)
        {
            bool resultado = false;

            try
            {
                // Devuelve un objeto Usuario para comparar si ya existe nombreUsuario
                Usuario user = GetUsuarios(usuario.NombreUsuario);

                
                
                if (user.NombreUsuario == null) // Comprobamos si existe nombreUsuario
                {
                    //Si no existe NombreUsuario comienza el alta del usuario pasado
                    using (SqlConnection conn = new SqlConnection(CadenaConn))
                    {
                        string queryCommand = "INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES(@nombre, @apellido, @nombreUsuario, @contraseña, @mail);";

                        //se crean los parametros necesarios como para ejecutar la consulta a la base de datos
                        SqlParameter parametroNombre = new SqlParameter("nombre", System.Data.SqlDbType.VarChar);
                        parametroNombre.Value = usuario.Nombre;
                        SqlParameter parametroApellido = new SqlParameter("apellido", System.Data.SqlDbType.VarChar);
                        parametroApellido.Value = usuario.Apellido;
                        SqlParameter parametroNombreUsuario = new SqlParameter("nombreUsuario", System.Data.SqlDbType.VarChar);
                        parametroNombreUsuario.Value = usuario.NombreUsuario;
                        SqlParameter parametroContraseña = new SqlParameter("contraseña", System.Data.SqlDbType.VarChar);
                        parametroContraseña.Value = usuario.Contraseña;
                        SqlParameter parametroMail = new SqlParameter("mail", System.Data.SqlDbType.VarChar);
                        parametroMail.Value = usuario.Mail;

                        conn.Open();
                        using (SqlCommand sqlcomm = new SqlCommand(queryCommand, conn))
                        {
                            sqlcomm.Parameters.Add(parametroNombre);
                            sqlcomm.Parameters.Add(parametroApellido);
                            sqlcomm.Parameters.Add(parametroNombreUsuario);
                            sqlcomm.Parameters.Add(parametroContraseña);
                            sqlcomm.Parameters.Add(parametroMail);

                            int filas = sqlcomm.ExecuteNonQuery();// se realiza el alta del usuario en la base de datos
                            if (filas > 0)
                            {
                                //Usuario creado
                                return true;
                            }



                        }

                        conn.Close();


                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }

            if(resultado)
            {
               Console.WriteLine("Usuario Dado de alta exitosamente");
            }
            {
                Console.WriteLine("No se puede repetir el nombre de usuario");
            }
            return resultado;

        }

        //Metodo para modificar datos de usuario
        public static bool ModificarUsuario(Usuario usuario)
        {
            bool resultado = false;

            try //Comandos a la base de datos para modificar al usuario como referencia por el id
            {
                
                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    //Consulta a la base de datos
                    string queryCommand = "UPDATE Usuario SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Contraseña = @contraseña, Mail = @mail WHERE Id = @id;";

                    //se crean los parametros necesarios como para ejecutar la consulta a la base de datos
                    SqlParameter parametroId = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                    parametroId.Value = usuario.Id;
                    SqlParameter parametroNombre = new SqlParameter("nombre", System.Data.SqlDbType.VarChar);
                    parametroNombre.Value = usuario.Nombre;
                    SqlParameter parametroApellido = new SqlParameter("apellido", System.Data.SqlDbType.VarChar);
                    parametroApellido.Value = usuario.Apellido;
                    SqlParameter parametroNombreUsuario = new SqlParameter("nombreUsuario", System.Data.SqlDbType.VarChar);
                    parametroNombreUsuario.Value = usuario.NombreUsuario;
                    SqlParameter parametroContraseña = new SqlParameter("contraseña", System.Data.SqlDbType.VarChar);
                    parametroContraseña.Value = usuario.Contraseña;
                    SqlParameter parametroMail = new SqlParameter("mail", System.Data.SqlDbType.VarChar);
                    parametroMail.Value = usuario.Mail;

                    conn.Open();
                    using (SqlCommand sqlcomm = new SqlCommand(queryCommand, conn))
                    {
                        sqlcomm.Parameters.Add(parametroId);
                        sqlcomm.Parameters.Add(parametroNombre);
                        sqlcomm.Parameters.Add(parametroApellido);
                        sqlcomm.Parameters.Add(parametroNombreUsuario);
                        sqlcomm.Parameters.Add(parametroContraseña);
                        sqlcomm.Parameters.Add(parametroMail);

                        int filas = sqlcomm.ExecuteNonQuery();//se realiza la modificacion del usuario
                        if (filas > 0)
                        {
                           resultado=true;//Modificacion realizada
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

 



