using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
/*
 * Controlador de la vista de registros
 * @author Jmenabc
 */
namespace WebApplication1.Controllers
{
    public class RegisterController : Controller
    {
        // GET: RegisterController
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(String name, String password)
        {
            //Recogemos la información de la vista
            ViewBag.Name = name;
            ViewBag.Password = password;
            using var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=root");
            Console.WriteLine("HABRIENDO CONEXION");
            connection.Open();
            
            NpgsqlCommand consulta = new NpgsqlCommand($"INSERT INTO \"public\".\"users\" (usuario_nick, usuario_password) VALUES('{ViewBag.Name}','{ViewBag.Password}')", connection);
            //Comparamos si la contraseña tiene menos de 7 caracteres
            bool mayuscula = false, minuscula = false, numero = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (Char.IsUpper(password, i))
                {
                    mayuscula = true;
                }
                else if (Char.IsLower(password, i))
                {
                    minuscula = true;
                }
                else if (Char.IsDigit(password, i))
                {
                    numero = true;
                }
            }
            if (mayuscula && minuscula && numero && password.Length >= 7)
            {
                Console.WriteLine("La contraseña cumple los requisitos minimos");
                NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();
                Console.WriteLine("Se ha registrado con exito");
            } else
            {
                Console.WriteLine("La contraseña no cumple los requisitos minimos");
            }
            Console.WriteLine("Cerrando conexion");
            connection.Close();
            return View();
        }

       
    }
}
