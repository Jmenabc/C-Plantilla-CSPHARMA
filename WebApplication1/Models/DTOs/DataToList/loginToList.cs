using DAL.Modelos;
using Npgsql;

namespace WebApplication1.Models.DTOs.DataToList
{
    public class loginToList
    {

        //metodo para convertir el resultado de la query en Lista
        public static List<User> ReaderToList(NpgsqlDataReader resultadoConsulta)
        {
            List<User> UserData = new List<User>();
            while (resultadoConsulta.Read())
            {

                UserData.Add(new User(

                        resultadoConsulta[0].ToString(),
                        resultadoConsulta[1].ToString(),
                        resultadoConsulta[2].ToString(),
                        resultadoConsulta[3].ToString()
                    ));

            }
            return UserData;
        }
    }
}
