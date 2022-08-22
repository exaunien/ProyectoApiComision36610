using MiPrimeraApi.Model;
using System.Data.SqlClient;

namespace MiPrimeraApi.Repository
{
    public static class UsuarioHandler
    {
        public const string CadenaConn = "Server=DESKTOP-86FO44B;Initial Catalog=SistemaGestion;" +
            "Trusted_Connection=true";
        
        public static List<Usuario> GetUsuarios()
        {
            const string querycommand = "SELECT * FROM Usuario";

            List<Usuario> resultado = new List<Usuario>();

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
                                    Usuario usuario = new Usuario();
                                    usuario.Id = Convert.ToInt32(lector["Id"]);
                                    usuario.Nombre = lector["Nombre"].ToString();
                                    usuario.Apellido = lector["Apellido"].ToString();
                                    usuario.NombreUsuario = lector["NombreUsuario"].ToString();
                                    usuario.Contraseña = lector["Contraseña"].ToString();
                                    usuario.Mail = lector["Mail"].ToString();
                                    resultado.Add(usuario);
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
        public static Usuario VerificaUsuario(string nombreUsuario, string contraseña)
        {
            
            string queryCommand = "SELECT * FROM Usuario WHERE Usuario.NombreUsuario = @nombreUsuario AND Usuario.Contraseña = @contraseña;";
            Usuario resultado = new Usuario();  
            try
            {
                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    SqlParameter parametroUser = new SqlParameter();
                    parametroUser.ParameterName = "nombreUsuario";
                    parametroUser.SqlDbType = System.Data.SqlDbType.VarChar;
                    parametroUser.Value = nombreUsuario;
                    SqlParameter parametroContraseña = new SqlParameter();
                    parametroContraseña.ParameterName = "contraseña";
                    parametroContraseña.SqlDbType = System.Data.SqlDbType.VarChar;
                    parametroContraseña.Value = contraseña;

                    
                    
                    conn.Open();

                    using (SqlCommand sqlcomm = new SqlCommand(queryCommand, conn))
                    {
                        sqlcomm.Parameters.Add(parametroUser);
                        sqlcomm.Parameters.Add(parametroContraseña);
                       using (SqlDataReader lector = sqlcomm.ExecuteReader())
                        {
                            if (lector.HasRows)
                            {
                                while (lector.Read())
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
            return resultado;
        }

        public static bool BajaUsuario (int id)
        {
            bool resultado = false;
           

            try
            {

                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    string queryCommand = "DELETE FROM Usuario WHERE Id = @idUsuario;";
                    conn.Open();
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

        public static bool AltaUsuario(Usuario usuario)
        {
            bool resultado = false;
            
            try
            {
                
                using(SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    string queryCommand = "INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES(@nombre, @apellido, @nombreUsuario, @contraseña, @mail);";
                    SqlParameter parametroNombre = new SqlParameter("nombre", System.Data.SqlDbType.VarChar);
                    parametroNombre.Value = usuario.Nombre;
                    SqlParameter parametroApellido = new SqlParameter("apellido", System.Data.SqlDbType.VarChar);
                    parametroApellido.Value = usuario.Apellido;
                    SqlParameter parametroNombreUsuario = new SqlParameter("nombreUsuario", System.Data.SqlDbType.VarChar);
                    parametroNombreUsuario.Value = usuario.NombreUsuario;
                    SqlParameter parametroContraseña = new SqlParameter("contraseña", System.Data.SqlDbType.VarChar);
                    parametroContraseña.Value = usuario.Contraseña;
                    SqlParameter parametroMail = new SqlParameter("mail", System.Data.SqlDbType.VarChar);
                    parametroMail.Value =usuario.Mail;

                    conn.Open();
                    using (SqlCommand sqlcomm = new SqlCommand(queryCommand, conn))
                    {
                        sqlcomm.Parameters.Add(parametroNombre);
                        sqlcomm.Parameters.Add(parametroApellido);
                        sqlcomm.Parameters.Add(parametroNombreUsuario);
                        sqlcomm.Parameters.Add(parametroContraseña);
                        sqlcomm.Parameters.Add(parametroMail);

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

        public static bool ModificarUsuario(Usuario usuario)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection conn = new SqlConnection(CadenaConn))
                {
                    string queryCommand = "UPDATE Usuario SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Contraseña = @contraseña, Mail = @mail WHERE Id = @id;";
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

                        int filas = sqlcomm.ExecuteNonQuery();
                        if (filas > 0)
                        {
                           resultado=true;
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

 



