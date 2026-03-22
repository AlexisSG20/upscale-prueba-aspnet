namespace SistemaAccesoWeb.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;

        public string Nombres { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;

        public string? CorreoPrincipal { get; set; }
        public string? CorreoSecundario { get; set; }
        public string? TelefonoMovil { get; set; }
        public string? TelefonoSecundario { get; set; }

        public DateTime? FechaNacimiento { get; set; }
        public string? Nacionalidad { get; set; }
        public string? Sexo { get; set; }
        public string? TipoContratacion { get; set; }
        public DateTime? FechaContratacion { get; set; }
        public string? Entidad { get; set; }
        public string? Rol { get; set; }

        public int IntentosFallidos { get; set; }
        public bool EstaBloqueado { get; set; }
        public DateTime? BloqueadoHasta { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}