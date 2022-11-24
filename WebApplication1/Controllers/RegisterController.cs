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
            if (password.Length < 7)
            {
                Console.WriteLine("La contraseña tiene que tener al menos 7 caracteres de longitud");
            }
            //Comprobamos si la contraseña tiene mayusculas
            else if(!password.Contains("A,B,C,D,E,F,G,H,I,J,K,L,M,N,Ñ,O,P,Q,R,S,T,U,V,W,X,Y,Z"))
            {
                Console.WriteLine("La contraseña tiene que tener al menos 1 mayúscula");
            }
            //Comprobamos si la contraseña tiene minúscula
            else if(!password.Contains("a,b,c,d,e,f,g,h,i,j,k,l,m,n,ñ,o,p,q,r,s,t,u,v,w,x,y,z"))
            {
                Console.WriteLine("La contraseña tiene que tener al menos 1 minúscula");
            }
            else
            {
                NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();
                Console.WriteLine("Se ha registrado con exito");

            }




            Console.WriteLine("Cerrando conexion");
            connection.Close();
            return View();
        }


    }
}
