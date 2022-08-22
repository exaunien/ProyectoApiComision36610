using MiPrimeraApi.Model;
using System.Data.SqlClient;

namespace MiPrimeraApi.Repository
{
    public static class VentaHandler
    {
        public const string CadenaConn = "Server=DESKTOP-86FO44B;Initial Catalog=SistemaGestion;" +
            "Trusted_Connection=true";
        public static List<Venta> GetVentas()
        {
            const string querycommand = "SELECT * FROM Venta";

            List<Venta> resultado = new List<Venta>();

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
                                    Venta ventas = new Venta();
                                    ventas.Id = Convert.ToInt32(lector["Id"]);
                                    ventas.Comentarios = lector["Comentarios"].ToString();
                                    resultado.Add(ventas);
                                }
                            }
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
    }
}
