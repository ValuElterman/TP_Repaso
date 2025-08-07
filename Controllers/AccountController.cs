using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoRepaso.Models;
using Newtonsoft.Json;

namespace ProyectoRepaso.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    public IActionResult Login(int IdUsuario, string nombre, string apellido, string foto, string username, DateTime ultimoLogin, string password)
    {
        Usuario usu = new Usuario (IdUsuario, nombre, apellido, foto, username, ultimoLogin, password);
        HttpContext.Session.SetString("usu", Objeto.ObjectToString(usu));
        return View("Login");
    }

    [HttpPost]
    public IActionResult GuardarLogin(string username, string password)
    {
        //prinicpio de session
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ViewBag.Error = "Por favor, complete todos los campos.";
            return View("Login");
        }

        usu = BD.Login(username, password);

        if(usu == null)
        {
                ViewBag.No = "Tu usuario no existe, por favor registrate.";
                return View("Registro");
        }
        else
        {
                HttpContext.Session.SetString("usu", Objeto.ObjectToString(usu));
                return RedirectToAction("Index", "Home");
        }
    }
    public IActionResult CerrarSesion()
    {
        //HACER
        return View("Login");
    }
    public IActionResult Registro()
    {
        return View("Registro");
    }
    public IActionResult GuardarRegistro(string username, string password, string nombre, string apellido, string foto)
    { //prinicpio de session
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(foto))
        {
            ViewBag.Error = "Por favor, complete todos los campos.";
            return View("Registro");
        }
        ultimoLogin = DateTime.Now;
        Usuario user = new Usuario (username, password, nombre, apellido, foto, ultimoLogin);
        usu = BD.ValidarRegistro(user);
        if(usu == false)
        {
            usu = BD.Registrarse(user);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewBag.Ya = "Este usuario ya esta registrado, inicie sesion.";
            return View("Login");
        }
    }
}