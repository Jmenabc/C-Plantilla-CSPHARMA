using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Diagnostics;
using System.Xml.Linq;
using WebApplication1.Models;
/*
 * Controlador de la vista de Login
 * @author Jmenabc
 */
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Metodo post para comprobar si los credenciales de inicio de sesión son correctos

        [HttpPost]    
        public IActionResult Index(string name, string password)
        {

            //Recogemos la información de la vista
            ViewBag.Name = name;
            ViewBag.Password = password;
            Console.WriteLine(ViewBag.Name);
            //Hacemos la conexion
            using var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=root");
            Console.WriteLine("HABRIENDO CONEXION");
            connection.Open();
            
            NpgsqlCommand consulta = new NpgsqlCommand($"SELECT * FROM \"public\".\"users\" WHERE usuario_nick='{name}' AND usuario_password='{password}'", connection);
            NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();
            

            if (resultadoConsulta.HasRows)
            {
                connection.Close();
                connection.Open();
                resultadoConsulta.GetChar(3);
                Console.WriteLine("Datos correctos");
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("_User")))
                {
                    HttpContext.Session.SetString("_User", name);
                }
                return View("HomePage");

            }
            else
            {
                Console.WriteLine("Recuerde sus credenciales");
            }
            Console.WriteLine("Cerrando conexion");
            connection.Close();
            return View();
        }

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
            }
            else
            {
                Console.WriteLine("La contraseña no cumple los requisitos minimos");
            }
            Console.WriteLine("Cerrando conexion");
            connection.Close();
            return View();
        }


    }
}