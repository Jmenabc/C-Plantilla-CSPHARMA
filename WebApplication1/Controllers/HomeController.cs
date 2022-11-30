﻿using DAL.Modelos;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Xml.Linq;
using WebApplication1.Models;
using WebApplication1.Models.DTOs.DataToList;
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

        //Metodo post para comprobar si los credenciales de inicio de sesión son correctos

        [HttpPost]    
        public IActionResult Index(string name, string password)
        {
            //Hacemos la conexion
            using var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=root");
            Console.WriteLine("HABRIENDO CONEXION");
            connection.Open();
            //Declaramos la lista
            List<DlkCatAccEmpleado> dataUser = new List<DlkCatAccEmpleado>();
            //Hacemos la consulta y guardamos su información
            NpgsqlCommand consulta = new NpgsqlCommand($"SELECT * FROM \"public\".\"dlk_cat_acc_empleado\" WHERE cod_empleado='{name}' AND clave_empleado='{password}'",connection);
            NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();
            //Metemos los valores de la consulta en una lista para poder acceder a ella
            dataUser = UserResponseToList.ReaderToList(resultadoConsulta);
            if (resultadoConsulta.HasRows)
            {
                //Metemos los valores en ViewBags para pasarlos a la vista
                ViewBag.IsAdmin = dataUser[0].NivelAccesoEmpleado;
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
            //Cerramos la conexión
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