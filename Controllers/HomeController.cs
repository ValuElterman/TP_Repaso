using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoRepaso.Models;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ProyectoRepaso.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    { 
        return RedirectToAction("Login", "Account");
    }

    public IActionResult Comenzar(string titulo, string descripcion, DateTime fecha, bool finalizada)
    {
        Tarea tarea = new Tarea (titulo, descripcion, fecha, finalizada);
        HttpContext.Session.SetString("tar", Objeto.ObjectToString(tarea));
        return View();
    }

    public IActionResult VerTareas(int IdUsuario)
    { 
        List<Tarea> listaTareas = BD.DevolverTareas(IdUsuario);
        ViewBag.listaTareas = listaTareas;
        HttpContext.Session.SetString("lista", Objeto.ListToString(listaTareas));
        return View("VerTareas");
    }
    public IActionResult CrearTarea()
    {
        return View("CrearTareas");
    }

    [HttpPost]
    public IActionResult GuardarTareaCreada(string titulo, string descripcion, DateTime fecha, bool finalizada)
    {
        Tarea tarea = new Tarea (titulo, descripcion, fecha, finalizada);
        BD.CrearTarea(tarea);
        ViewBag.tareaCreada = "La tarea fue creada con exito";
        Usuario usu = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("usu"));
        List<Tarea> listaTareas = BD.DevolverTareas(usu.IdUsuario);
        HttpContext.Session.SetString("listaTareas", Objeto.ListToString(listaTareas));
        return View ("VertTareas");
    }
    public IActionResult EditarTarea(int id)
    {
        Tarea tareaAEditar = BD.DevolverTarea(id);
        if(tareaAEditar == null)
        {
            ViewBag.Error = "no se encontro la tarea";
        }
        return View("ModificarTareas", tareaAEditar);
    }
    public IActionResult GuardarTareaEditada (string titulo, string descripcion, DateTime fecha, bool finalizada)
    {
        Tarea tarea = new Tarea (titulo, descripcion, fecha, finalizada);
        BD.ModificarTarea(tarea);
        ViewBag.exito = "La tarea fue modificada";
        HttpContext.Session.SetString("tar", Objeto.ObjectToString(tarea));
        return View ("VerTareas");
    }
    public IActionResult EliminarTarea(int id)
    {
        Tarea tareaAEditar = BD.DevolverTarea(id);
        if(tareaAEditar == null)
        {
            ViewBag.Error = "no se encontro la tarea";
        }
        else
        {
            BD.EliminarTarea(id);
            ViewBag.exito = "La tarea fue eliminada";
        }
        return View("VerTareas");
    }
    public IActionResult FinalizarTarea(int id)
    {
        Tarea tareaAFinalizar = BD.DevolverTarea(id);
        if(tareaAFinalizar == null)
        {
            ViewBag.Error = "no se encontro la tarea";
        }
        return View("ModificarTareas");
    }
}
