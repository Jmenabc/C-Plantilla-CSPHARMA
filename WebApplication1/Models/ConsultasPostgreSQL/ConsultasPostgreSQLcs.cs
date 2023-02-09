using DAL.Modelos;
using Npgsql;
using System.Xml.Linq;
using WebApplication1.Models.DTOs;
/*
 * Clase que contiene todas nuestras consultasSQL
 *@author Jmenabc 
 */

namespace WebApplication1.Models.ConsultasPostgreSQL
{
    public class ConsultasPostgreSQLcs
    {

        public static List<DlkCatAccEmpleadoDTOcs> listaDeEmpleadosLogin(IConfiguration _config, string name, string password)
        {
            Console.WriteLine("[Modelos-ConsultasPostgreSQL-ConsultasPostgreSQL-listaDeEmpleadosLogin]: Entrando al metodo");
            //Hacemos la conexion
            using var connection = new NpgsqlConnection(_config.GetConnectionString("EFCConexion"));
            connection.Open();
            Console.WriteLine("[Modelos-ConsultasPostgreSQL-ConsultasPostgreSQL-listaDeEmpleadosLogin]: Abriendo conexion");
            Console.WriteLine("[Modelos-ConsultasPostgreSQL-ConsultasPostgreSQL-listaDeEmpleadosLogin]: Hacemos y ejecutamos la consulta");
            //ConsultaSQL
            NpgsqlCommand consulta = new NpgsqlCommand($"SELECT * FROM \"public\".\"dlk_cat_acc_empleado\" WHERE cod_empleado='{name}' AND clave_empleado='{password}'", connection);
            NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();
            //Metemos los valores recibidos en una lista
            
        }
    }
}
