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
        //añadimos nuestro archivo de configuracion json para recoger la cadena de conexion
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }


        //Metodo post para comprobar si los credenciales de inicio de sesión son correctos

        [HttpPost]    
        public IActionResult Index(string name, string password)
        {
            
            //Declaramos la lista
            List<DlkCatAccEmpleado> dataUser = new List<DlkCatAccEmpleado>();
            //Hacemos la consulta y guardamos su información
            
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

            NpgsqlCommand consulta = new NpgsqlCommand($"INSERT INTO \"public\".\"users\" (usuario_nick, usuario_password) VALUES('{name}','{password}')", connection);
            //Comparamos si la contraseña tiene menos de 7 caracteres
            bool mayuscula = false, minuscula = false, numero = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (Char.IsUpper(password, i))
                {
                    ViewBag.NotLower = "Al menos tiene que tener una minúscula";
                    ViewBag.NotDigit = "Al menos debe de tener un número";
                    mayuscula = true;
                    return View("Register");
                }
                else if (Char.IsLower(password, i))
                {
                    ViewBag.NotUpper = "Al menos tiene que tener una mayúscula";
                    ViewBag.NotDigit = "Al menos debe de tener un número";
                    minuscula = true;
                    return View("Register");
                }
                else if (Char.IsDigit(password, i))
                {
                    ViewBag.NotUpper = "Al menos tiene que tener una mayúscula";
                    ViewBag.NotLower = "Al menos tiene que tener una minúscula";
                    numero = true;
                    return View("Register");
                } 
            }
            if (mayuscula && minuscula && numero && password.Length >= 7)
            {

                Console.WriteLine("La contraseña cumple los requisitos minimos");
                NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();
                Console.WriteLine("Se ha registrado con exito");
                return View("HomePage");
            }
            else
            {
                ViewBag.Lenght = "La longitud minima debe ser de 7 caracteres";
                Console.WriteLine("La contraseña no cumple los requisitos minimos");
                return View("Register");
            }
            Console.WriteLine("Cerrando conexion");
            connection.Close();
            return View();
        }


    }
}