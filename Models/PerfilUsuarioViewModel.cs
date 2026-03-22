namespace SistemaAccesoWeb.Models
{
    public class PerfilUsuarioViewModel
    {
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public string CorreoPrincipal { get; set; } = string.Empty;
        public string? CorreoSecundario { get; set; }
        public string TelefonoMovil { get; set; } = string.Empty;
        public string? TelefonoSecundario { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string TipoContratacion { get; set; } = string.Empty;
        public DateTime? FechaContratacion { get; set; }
        public string Entidad { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}