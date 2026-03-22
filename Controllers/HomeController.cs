using Microsoft.AspNetCore.Mvc;
using SistemaAccesoWeb.Data;
using SistemaAccesoWeb.Models;
using System.Diagnostics;

namespace SistemaAccesoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;

        public HomeController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GestionUsuarios()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult GestionUsuarios(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = _usuarioRepository.ObtenerPorNumeroDocumento(model.NumeroDocumento);

            if (usuario == null)
            {
                model.MensajeError = "El usuario no existe.";
                return View(model);
            }

            if (usuario.EstaBloqueado && usuario.BloqueadoHasta.HasValue && usuario.BloqueadoHasta > DateTime.Now)
            {
                return RedirectToAction("CuentaBloqueada");
            }

            if (usuario.Contrasena != model.Contrasena)
            {
                _usuarioRepository.IncrementarIntentosFallidos(usuario.Id);

                var usuarioActualizado = _usuarioRepository.ObtenerPorNumeroDocumento(model.NumeroDocumento);

                if (usuarioActualizado != null && usuarioActualizado.IntentosFallidos >= 5)
                {
                    _usuarioRepository.BloquearUsuario(usuario.Id, DateTime.Now.AddMinutes(15));
                    return RedirectToAction("CuentaBloqueada");
                }

                model.MensajeError = "La contraseña es incorrecta.";
                return View(model);
            }

            _usuarioRepository.ReiniciarIntentosFallidos(usuario.Id);

            return RedirectToAction("PerfilUsuario", new { numeroDocumento = usuario.NumeroDocumento });
        }

        public IActionResult ProbarConexion()
        {
            var usuario = _usuarioRepository.ObtenerPorNumeroDocumento("46844596");

            if (usuario == null)
            {
                return Content("Usuario no encontrado");
            }

            return Content($"Usuario encontrado: {usuario.Nombres} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}");
        }
        public IActionResult CuentaBloqueada()
        {
            return View();
        }

        public IActionResult PerfilUsuario(string numeroDocumento)
        {
            var usuario = _usuarioRepository.ObtenerPorNumeroDocumento(numeroDocumento);

            if (usuario == null)
            {
                return RedirectToAction("GestionUsuarios");
            }

            var model = new PerfilUsuarioViewModel
            {
                TipoDocumento = usuario.TipoDocumento ?? string.Empty,
                NumeroDocumento = usuario.NumeroDocumento ?? string.Empty,
                Nombres = usuario.Nombres ?? string.Empty,
                ApellidoPaterno = usuario.ApellidoPaterno ?? string.Empty,
                ApellidoMaterno = usuario.ApellidoMaterno ?? string.Empty,
                CorreoPrincipal = usuario.CorreoPrincipal ?? string.Empty,
                CorreoSecundario = usuario.CorreoSecundario,
                TelefonoMovil = usuario.TelefonoMovil ?? string.Empty,

                TipoTelefonoSecundario = string.IsNullOrWhiteSpace(usuario.TelefonoSecundario) ? "Tipo" : "Casa",
                TelefonoSecundario = usuario.TelefonoSecundario,

                FechaNacimiento = usuario.FechaNacimiento,
                Nacionalidad = usuario.Nacionalidad ?? string.Empty,
                Sexo = usuario.Sexo ?? string.Empty,
                TipoContratacion = usuario.TipoContratacion ?? string.Empty,
                FechaContratacion = usuario.FechaContratacion,
                Entidad = usuario.Entidad ?? string.Empty,
                Rol = usuario.Rol ?? string.Empty,
                Estado = usuario.Estado ?? string.Empty
            };

            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}