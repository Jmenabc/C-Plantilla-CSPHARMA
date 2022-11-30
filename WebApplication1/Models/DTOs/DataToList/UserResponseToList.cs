using DAL.Modelos;
using Npgsql;

namespace WebApplication1.Models.DTOs.DataToList
{
    public class UserResponseToList
    {
        //metodo para convertir el resultado de la query en Lista
        public static List<DlkCatAccEmpleado> ReaderToList(NpgsqlDataReader resultadoConsulta)
        {
            List<DlkCatAccEmpleado> UserData = new List<DlkCatAccEmpleado>();
            while (resultadoConsulta.Read())
            {

                UserData.Add(new DlkCatAccEmpleado(

                        resultadoConsulta[0].ToString(),
                        resultadoConsulta[1].ToString(),
                        resultadoConsulta[2].ToString(),
                        resultadoConsulta[3].ToString(),
                        resultadoConsulta[4].ToString()
                    ));

            }
            return UserData;
        }
    }
}
