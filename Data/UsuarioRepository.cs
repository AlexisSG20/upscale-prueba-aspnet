using Microsoft.Data.SqlClient;
using SistemaAccesoWeb.Models;

namespace SistemaAccesoWeb.Data
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public Usuario? ObtenerPorNumeroDocumento(string numeroDocumento)
        {
            Usuario? usuario = null;

            using SqlConnection connection = new SqlConnection(_connectionString);
            string query = @"
                SELECT 
                    Id,
                    TipoDocumento,
                    NumeroDocumento,
                    Contrasena,
                    Nombres,
                    ApellidoPaterno,
                    ApellidoMaterno,
                    CorreoPrincipal,
                    CorreoSecundario,
                    TelefonoMovil,
                    TelefonoSecundario,
                    FechaNacimiento,
                    Nacionalidad,
                    Sexo,
                    TipoContratacion,
                    FechaContratacion,
                    Entidad,
                    Rol,
                    IntentosFallidos,
                    EstaBloqueado,
                    BloqueadoHasta,
                    Estado
                FROM Usuarios
                WHERE NumeroDocumento = @NumeroDocumento";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NumeroDocumento", numeroDocumento);

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                usuario = new Usuario
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    TipoDocumento = reader["TipoDocumento"].ToString() ?? string.Empty,
                    NumeroDocumento = reader["NumeroDocumento"].ToString() ?? string.Empty,
                    Contrasena = reader["Contrasena"].ToString() ?? string.Empty,
                    Nombres = reader["Nombres"].ToString() ?? string.Empty,
                    ApellidoPaterno = reader["ApellidoPaterno"].ToString() ?? string.Empty,
                    ApellidoMaterno = reader["ApellidoMaterno"].ToString() ?? string.Empty,
                    CorreoPrincipal = reader["CorreoPrincipal"] as string,
                    CorreoSecundario = reader["CorreoSecundario"] as string,
                    TelefonoMovil = reader["TelefonoMovil"] as string,
                    TelefonoSecundario = reader["TelefonoSecundario"] as string,
                    FechaNacimiento = reader["FechaNacimiento"] == DBNull.Value ? null : Convert.ToDateTime(reader["FechaNacimiento"]),
                    Nacionalidad = reader["Nacionalidad"] as string,
                    Sexo = reader["Sexo"] as string,
                    TipoContratacion = reader["TipoContratacion"] as string,
                    FechaContratacion = reader["FechaContratacion"] == DBNull.Value ? null : Convert.ToDateTime(reader["FechaContratacion"]),
                    Entidad = reader["Entidad"] as string,
                    Rol = reader["Rol"] as string,
                    IntentosFallidos = Convert.ToInt32(reader["IntentosFallidos"]),
                    EstaBloqueado = Convert.ToBoolean(reader["EstaBloqueado"]),
                    BloqueadoHasta = reader["BloqueadoHasta"] == DBNull.Value ? null : Convert.ToDateTime(reader["BloqueadoHasta"]),
                    Estado = reader["Estado"].ToString() ?? string.Empty
                };
            }

            return usuario;
        }
        public void IncrementarIntentosFallidos(int usuarioId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string query = @"
        UPDATE Usuarios
        SET IntentosFallidos = IntentosFallidos + 1
        WHERE Id = @Id";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", usuarioId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void ReiniciarIntentosFallidos(int usuarioId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string query = @"
        UPDATE Usuarios
        SET IntentosFallidos = 0,
            EstaBloqueado = 0,
            BloqueadoHasta = NULL
        WHERE Id = @Id";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", usuarioId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void BloquearUsuario(int usuarioId, DateTime bloqueadoHasta)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string query = @"
        UPDATE Usuarios
        SET EstaBloqueado = 1,
            BloqueadoHasta = @BloqueadoHasta
        WHERE Id = @Id";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", usuarioId);
            command.Parameters.AddWithValue("@BloqueadoHasta", bloqueadoHasta);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}