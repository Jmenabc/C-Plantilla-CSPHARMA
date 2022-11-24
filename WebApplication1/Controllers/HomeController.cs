using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Diagnostics;
using System.Xml.Linq;
using WebApplication1.Models;

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

        [HttpPost]    
        public IActionResult Index(String name, String password)
        {
            ViewBag.Name = name;
            ViewBag.Password = password;
            Console.WriteLine(ViewBag.Name);
            using var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=root");
            Console.WriteLine("HABRIENDO CONEXION");
            connection.Open();
            //Recogemos la información de la vista
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