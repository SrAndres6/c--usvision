using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using proyecto_c_.Models;
using proyecto_c_.Data;
using System.Linq;

namespace proyecto_c_.Controllers
{
    public class GruposController : Controller
    {
        public IActionResult Index()
        {
            // 🔒 Solo administrador puede ver el listado
            if (HttpContext.Session.GetString("UsuarioLogueado") != "jhon" ||
                HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede acceder a Grupos.";
                return RedirectToAction("Index", "Login");
            }

            return View(AppData.Grupos);
        }

        public IActionResult Crear()
        {
            // 🔒 Validación de rol
            if (HttpContext.Session.GetString("UsuarioLogueado") != "jhon" ||
                HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede crear.";
                return RedirectToAction("Index", "Home");
            }

            return View(new Grupo()); // 🔥 inicializa modelo para evitar null
        }

        [HttpPost]
        public IActionResult Crear(Grupo grupo)
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") != "jhon" ||
                HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede crear.";
                return RedirectToAction("Index", "Home");
            }

            grupo.Id = AppData.Grupos.Count + 1;
            AppData.Grupos.Add(grupo);
            AppData.GuardarTodo(); // 🔥 guarda automáticamente
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") != "jhon" ||
                HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede editar.";
                return RedirectToAction("Index", "Home");
            }

            var grupo = AppData.Grupos.FirstOrDefault(g => g.Id == id);
            if (grupo == null) return NotFound();
            return View(grupo);
        }

        [HttpPost]
        public IActionResult Editar(Grupo grupo)
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") != "jhon" ||
                HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede editar.";
                return RedirectToAction("Index", "Home");
            }

            var original = AppData.Grupos.FirstOrDefault(g => g.Id == grupo.Id);
            if (original != null)
            {
                original.IdMateria = grupo.IdMateria;
                original.IdDocente = grupo.IdDocente;
                original.IdHorario = grupo.IdHorario;

                AppData.GuardarTodo(); // 🔥 guarda cambios
            }
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") != "jhon" ||
                HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede eliminar.";
                return RedirectToAction("Index", "Home");
            }

            var grupo = AppData.Grupos.FirstOrDefault(g => g.Id == id);
            if (grupo == null) return NotFound();
            return View(grupo);
        }

        [HttpPost]
        public IActionResult EliminarConfirmado(int id)
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") != "jhon" ||
                HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede eliminar.";
                return RedirectToAction("Index", "Home");
            }

            var grupo = AppData.Grupos.FirstOrDefault(g => g.Id == id);
            if (grupo != null)
            {
                AppData.Grupos.Remove(grupo);
                AppData.GuardarTodo(); // 🔥 guarda después de eliminar
            }

            return RedirectToAction("Index");
        }
    }
}
