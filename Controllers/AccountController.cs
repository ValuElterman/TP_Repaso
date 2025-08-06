using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoRepaso.Models;

namespace ProyectoRepaso.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}