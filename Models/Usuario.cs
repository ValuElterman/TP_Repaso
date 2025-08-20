using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoRepaso.Models;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ProyectoRepaso.Models;

public class Usuario
{
    [JsonProperty]
    public int IdUsuario{get; set;}
    [JsonProperty]
   public string nombre{get; set;}
   [JsonProperty]
   public string apellido{get; set;}
   [JsonProperty]
   public string foto{get; set;}
   [JsonProperty]
   public string  username{get; set;}
   [JsonProperty]
   public DateTime ultimoLogin{get; set;}
   [JsonProperty]
    public string password{get; set;}

    public Usuario()
    {
        // Dapper lo necesita para instanciar el objeto.
    }
    public Usuario (string nombre, string apellido, string foto, string username, DateTime ultimoLogin, string password)
{
    this.nombre = nombre;
    this.apellido = apellido;
    this.foto = foto;
    this.username = username;
    this.ultimoLogin = ultimoLogin;
    this.password = password;
}
}