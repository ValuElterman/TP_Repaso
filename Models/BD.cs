using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Dapper;

namespace ProyectoRepaso.Models;

public static class BD
{
    private static string _connectionString = @"Server=localhost; DataBase=TP06; Integrated Security=True; TrustServerCertificate=True;";
   public void Registrarse(Usuario usuario)
   {
        string query = "INSERT INTO Usuarios (IdUsuario, nombre, apellido, foto, username, ultimoLogin, password) VALUES (@pIdUsuario, @pNombre, @pApellido, @pFoto, @pUsername, @pUltimoLogin, @pPassword)";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new {pIdUsuario = usuario.IdUsuario, pNombre = usuario.nombre, pApellido = usuario.apellido, pFoto = usuario.foto, pUsername = usuario.username, pUltimoLogin = usuario.ultimoLogin, pPassword = usuario.password});
        }
   }

   public bool ValidarRegistro(Usuario usuario)
   {
        bool existe = false;
        user = usuario.username;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT username FROM Usuarios WHERE username = @pUser";
            existe = connection.QueryFirstOrDefault<Usuario>(query, new {pUser = user});
        }
        return existe;
   }
   public Usuario Login (string username, string password)
   {
        Usuario usu = null;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT username, password FROM Usuarios WHERE username = @pUsername AND password = @pPassword";
            usu = connection.QueryFirstOrDefault<Usuario>(query, new {pUsername = username, pPassword = password});
        }
        return usu;
   }

   public List<Tarea> DevolverTareas (int IdUsuario)
   {
        List<Tarea> tareas = new List<Tarea>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "SELECT * FROM Tareas WHERE IdUsuario = @pIdUsuario";
        tareas = connection.Query<Usuario>(query, new {IdUsuario = pIdUsuario}).ToList();
    }
    return tareas;
   }

   public Tarea DevolverTarea (int IdTarea)
   {
        Tarea tarea = null;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas WHERE IdTarea = @pIdTarea";
            tarea = connection.QueryFirstOrDefault<Tarea>(query, new {IdTarea = pIdTarea});
        }
        return tarea;
   }

   public void ModificarTarea (Tarea tarea)
   {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Tareas SET";
            tarea = connection.QueryFirstOrDefault<Tarea>(query, new {IdTarea = pIdTarea});
        }
   }

   public void EliminarTarea (int IdTarea)
   {

   }

   public void CrearTarea (Tarea tarea)
   {

   }

   public void FinalizarTarea (int IdTarea)
   {

   }

   public void ActualizarLogin (int IdU)
   {

   }
}