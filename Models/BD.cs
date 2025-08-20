using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoRepaso.Models;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Dapper;
namespace ProyectoRepaso.Models;

public static class BD
{
    private static string _connectionString = @"Server=localhost; DataBase=TP06; Integrated Security=True; TrustServerCertificate=True;";
   public static void Registrarse(Usuario usuario)
   {
        string query = "INSERT INTO Usuarios (nombre, apellido, foto, username, ultimoLogin, password) VALUES (@pNombre, @pApellido, @pFoto, @pUsername, @pUltimoLogin, @pPassword)";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new {pNombre = usuario.nombre, pApellido = usuario.apellido, pFoto = usuario.foto, pUsername = usuario.username, pUltimoLogin = usuario.ultimoLogin, pPassword = usuario.password});
        }
   }
   public static bool ValidarRegistro(Usuario usuario)
   {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT IdUsuario FROM Usuarios WHERE username = @pUser";
            Usuario userExistente = connection.QueryFirstOrDefault<Usuario>(query, new { pUser = usuario.username });
            return userExistente != null;
        }
   }
   
   public static Usuario Login (string username, string password)
   {
        Usuario usu = null;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT username, password FROM Usuarios WHERE username = @pUsername AND password = @pPassword";
            usu = connection.QueryFirstOrDefault<Usuario>(query, new {pUsername = username, pPassword = password});
        }
        return usu;
   }

   public static List<Tarea> DevolverTareas (int IdUsuario)
   {
        List<Tarea> tareas = new List<Tarea>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas WHERE IdUsuario = @pIdUsuario";
            tareas = connection.Query<Tarea>(query, new {pIdUsuario = IdUsuario}).ToList();
        }
        return tareas;
   }

   public static Tarea DevolverTarea (int IdTarea)
   {
        Tarea tarea = null;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas WHERE IdTarea = @pIdTarea";
            tarea = connection.QueryFirstOrDefault<Tarea>(query, new {pIdTarea = IdTarea});
        }
        return tarea;
   }

   public static void ModificarTarea (Tarea tarea)
   {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Tareas SET titulo = @pTitulo, descripcion = @pDescripcion, fecha = @pFecha, finalizada = @pFinalizada WHERE IdTarea = @pIdTarea";
            connection.Execute(query, tarea);
        }
   }

   public static void EliminarTarea (int IdTarea)
   {
        string query = "DELETE FROM Tareas WHERE IdTarea = @pIdTarea";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new {IdTarea});
        }
   }

   public static void CrearTarea (Tarea tarea)
   { 
        string query = "INSERT INTO Tareas (titulo, descripcion, fecha, finalizada, IdUsuario) VALUES (@pTitulo, @pDescripcion, @pFecha, @pFinalizada, @pIdUsuario)";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new {pTitulo = tarea.titulo, pDescripcion = tarea.descripcion, pFecha = tarea.fecha, pFinalizada = tarea.finalizada, pIdUsuario = tarea.IdUsuario});
        }
   }

   public static void FinalizarTarea (int IdTarea)
   {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Tareas SET finalizada = @pFinalizada WHERE IdTarea = @pIdTarea";
            connection.Execute(query, new {pFinalizada = true, pIdTarea = IdTarea});
        }
   }

   public static void ActualizarLogin (int IdUsuario)
   {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        { //solo queremos actualizar la fecha
            string query = "UPDATE Usuarios SET ultimoLogin = @pUltimoLogin WHERE IdUsuario = @pIdUsuario";
            connection.Execute(query, new {pUltimoLogin = DateTime.Now, pIdUsuario = IdUsuario});
        }
   }

}