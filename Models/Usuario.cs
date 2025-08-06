using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Dapper;

namespace ProyectoRepaso.Models;

public class Usuario
{
    [JsonProperty]
    private int IdUsuario{get; set;}
    [JsonProperty]
   private string nombre{get; set;}
   [JsonProperty]
   private string apellido{get; set;}
   [JsonProperty]
   private string foto{get; set;}
   [JsonProperty]
   private string  username{get; set;}
   [JsonProperty]
   private DateTime ultimoLogin{get; set;}
   [JsonProperty]
    private string password{get; set;}
}