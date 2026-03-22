using System.ComponentModel.DataAnnotations;

namespace SistemaAccesoWeb.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Contrasena { get; set; } = string.Empty;

        public string TipoDocumento { get; set; } = "DNI";

        public string? MensajeError { get; set; }
    }
}