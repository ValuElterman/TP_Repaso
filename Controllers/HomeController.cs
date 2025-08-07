using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoRepaso.Models;
using Newtonsoft.Json;

namespace ProyectoRepaso.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(int IdTarea, string titulo, string descripcion, DateTime fecha, bool finalizada)
    { //no va aca esto
        Tarea tarea = new Tarea (IdTarea, titulo, descripcion, fecha, finalizada);
        HttpContext.Session.SetString("hola", Objeto.ObjectToString(tarea));
        return View();
    }

    public IActionResult VerTareas(int IdUsuario)
    { //ver si esta completo
        ViewBag.listaTareas = BD.DevolverTareas(IdUsuario);
        return View("VerTareas");
    }
    public IActionResult CrearTarea()
    {
        return View("CrearTareas");
    }
    public IActionResult GuardarTareaCreada()
    {
        
    }
    public IActionResult EditarTarea()
    {
        return View("ModificarTareas");
    }
    public IActionResult GuardarTareaEditada()
    {
        
    }
    public IActionResult EliminarTarea()
    {
        
    }
    public IActionResult FinalizarTarea()
    {
        return View("ModificarTareas");
    }
}
