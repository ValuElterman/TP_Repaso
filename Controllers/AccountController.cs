using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoRepaso.Models;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ProyectoRepaso.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    public IActionResult Login()
    {
        return View("Login");
    }

    public IActionResult Comenzar(string nombre, string apellido, string foto, string username, DateTime ultimoLogin, string password)
    {
        Usuario usu = new Usuario (nombre, apellido, foto, username, ultimoLogin, password);
        HttpContext.Session.SetString("usu", Objeto.ObjectToString(usu));
        return View("Login");
    }

    [HttpPost]
    public IActionResult GuardarLogin(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ViewBag.Error = "Por favor, complete todos los campos.";
            return View("Login");
        }

        Usuario usu = BD.Login(username, password);

        if(usu == null)
        {
            ViewBag.No = "Tu usuario no existe, por favor registrate.";
            return View("Login");
        }
        else
        {
            BD.ActualizarLogin(usu.IdUsuario);
            HttpContext.Session.SetString("usuId", usu.IdUsuario.ToString());
            return RedirectToAction("VerTareas", "Home");
        }
    }
    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return View("Login");
    }
    public IActionResult Registro()
    {
        return View("Registro");
    }
    public IActionResult GuardarRegistro(string nombre, string apellido, string foto, string username, DateTime ultimoLogin, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(foto))
        {
            ViewBag.Error = "Por favor, complete todos los campos.";
            return View("Registro");
        }
        ultimoLogin = DateTime.Now;
        Usuario newUser = new Usuario (nombre, apellido, foto, username, ultimoLogin, password);
        bool usu = BD.ValidarRegistro(newUser);
        if(usu == false)
        {
            BD.Registrarse(newUser);
            if(newUser.IdUsuario != null)
            {
                HttpContext.Session.SetString("usu", Objeto.ObjectToString(newUser));
                return RedirectToAction("Comenzar", "Home");
            }
            else
            {
                ViewBag.Error = "Ocurrió un error al registrarse. Inténtelo de nuevo.";
                return View("Registro");
            }
        }
        else
        {
            ViewBag.Ya = "Este usuario ya esta registrado, inicie sesion.";
            return View("Login");
        }
    }
}