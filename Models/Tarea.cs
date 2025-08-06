using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Dapper;

namespace ProyectoRepaso.Models;

public class Tarea
{
    [JsonProperty]
    private int IdTarea{get; set;}
    [JsonProperty]
    private string titulo{get; set;}
    [JsonProperty]
    private string descripcion{get; set;}
    [JsonProperty]
    private DateTime fecha{get; set;}
    [JsonProperty]
    private bool finalizada{get; set;}

}