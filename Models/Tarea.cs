using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoRepaso.Models;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ProyectoRepaso.Models;

public class Tarea
{
    [JsonProperty]
    public int IdTarea{get; set;}
    [JsonProperty]
    public string titulo{get; set;}
    [JsonProperty]
    public string descripcion{get; set;}
    [JsonProperty]
    public DateTime fecha{get; set;}
    [JsonProperty]
    public bool finalizada{get; set;}

    public int IdUsuario{get; set;}

 public Tarea()
    {
        
    }
public Tarea (string titulo, string descripcion, DateTime fecha, bool finalizada, int IdUsuario)
{
    this.titulo = titulo;
    this.descripcion = descripcion;
    this.fecha = fecha;
    this.finalizada = finalizada;
    this.IdUsuario = IdUsuario;
}
}