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

    public IActionResult Comenzar(string titulo, string descripcion, DateTime fecha, bool finalizada, int IdUsuario)
    {
        Tarea tarea = new Tarea (titulo, descripcion, fecha, finalizada, IdUsuario);
        HttpContext.Session.SetString("tar", Objeto.ObjectToString(tarea));
        return View("VerTareas");
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
        string idUsuarioString = HttpContext.Session.GetString("usuId");
        if (string.IsNullOrEmpty(idUsuarioString))
        {
            return RedirectToAction("Login", "Account");
        }
        if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(descripcion))
        {
            ViewBag.Error = "Por favor, complete los campos de título y descripción.";
            return View("CrearTareas");
        }
          if (fecha == default(DateTime))
            {
                fecha = DateTime.Now;
            }
        
        int idUsuario = int.Parse(idUsuarioString);
        Tarea tarea = new Tarea (titulo, descripcion, fecha, finalizada, idUsuario);
        BD.CrearTarea(tarea);

        ViewBag.tareaCreada = "La tarea fue creada con exito";

        List<Tarea> listaTareas = BD.DevolverTareas(idUsuario);
         ViewBag.listaTareas = listaTareas;
        return View ("VerTareas");
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
    public IActionResult GuardarTareaEditada (int IdTarea,string titulo, string descripcion, DateTime fecha, bool finalizada)
    {
        string idUsuarioString = HttpContext.Session.GetString("usuId");
        int idUsuario = int.Parse(idUsuarioString);

        Tarea tarea = new Tarea (titulo, descripcion, fecha, finalizada, idUsuario);
        tarea.IdTarea = IdTarea;
        BD.ModificarTarea(tarea);

        ViewBag.exito = "La tarea fue modificada con éxito";

        List<Tarea> listaTareas = BD.DevolverTareas(idUsuario);
        ViewBag.listaTareas = listaTareas;
        return View ("VerTareas");
    }
    public IActionResult EliminarTarea(int id)
    {
        Tarea tareaAEliminar = BD.DevolverTarea(id);
        if(tareaAEliminar == null)
        {
            ViewBag.Error = "No se encontró la tarea a eliminar";
        }
        else
        {
            BD.EliminarTarea(id);
            ViewBag.exito = "La tarea fue eliminada con éxito";
        }
        return RedirectToAction("VerTareas");
    }
    public IActionResult FinalizarTarea(int id)
    {
        Tarea tareaAFinalizar = BD.DevolverTarea(id);
        if(tareaAFinalizar == null)
        {
            ViewBag.Error = "no se encontro la tarea";
        }
        else
        {
            BD.FinalizarTarea(id);
            ViewBag.exito = "La tarea fue finalizada.";
        }
        string idUsuarioString = HttpContext.Session.GetString("usuId");
        int idUsuario = int.Parse(idUsuarioString);

        List<Tarea> listaTareas = BD.DevolverTareas(idUsuario);
        ViewBag.listaTareas = listaTareas;
        return View("VerTareas");
    }
}
