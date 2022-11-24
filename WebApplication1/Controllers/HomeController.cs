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
        public IActionResult Index(String name, String password)
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
                Console.WriteLine("Ha iniciado sesion con exito");
            }
            else
            {
                Console.WriteLine("Recuerde sus credenciales");
            }

            Console.WriteLine("Cerrando conexion");
            connection.Close();
            return View();
        }


    }
}