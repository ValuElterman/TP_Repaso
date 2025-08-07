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

    public IActionResult Login()
    {
        return View("Login");
    }

    [HttpPost]
    public IActionResult GuardarLogin()
    {

    }
    public IActionResult CerrarSesion()
    {
        
    }
    public IActionResult Registro()
    {
        return View("Registro");
    }
    public IActionResult GuardarRegistro()
    {
        
    }
}