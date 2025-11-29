using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using proyecto_c_.Models;
using proyecto_c_.Data;
using System.Linq;

namespace proyecto_c_.Controllers
{
    public class EstudiantesController : Controller
    {
        public IActionResult Index()
        {
            return View(AppData.Estudiantes);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Estudiante estudiante)
        {
            estudiante.Id = AppData.Estudiantes.Count + 1;
            AppData.Estudiantes.Add(estudiante);
            AppData.GuardarTodo(); // 🔥 guarda automáticamente
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede editar.";
                return RedirectToAction("Index", "Home");
            }

            var estudiante = AppData.Estudiantes.FirstOrDefault(e => e.Id == id);
            if (estudiante == null) return NotFound();
            return View(estudiante);
        }

        [HttpPost]
        public IActionResult Editar(Estudiante estudiante)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede editar.";
                return RedirectToAction("Index", "Home");
            }

            var original = AppData.Estudiantes.FirstOrDefault(e => e.Id == estudiante.Id);
            if (original != null)
            {
                original.Nombre = estudiante.Nombre;
                original.Programa = estudiante.Programa;
                AppData.GuardarTodo(); // 🔥 guarda cambios
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Eliminar(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede eliminar.";
                return RedirectToAction("Index", "Home");
            }

            var estudiante = AppData.Estudiantes.FirstOrDefault(e => e.Id == id);
            if (estudiante == null) return NotFound();
            return View(estudiante);
        }

        [HttpPost]
        public IActionResult EliminarConfirmado(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede eliminar.";
                return RedirectToAction("Index", "Home");
            }

            var estudiante = AppData.Estudiantes.FirstOrDefault(e => e.Id == id);
            if (estudiante != null)
            {
                AppData.Estudiantes.Remove(estudiante);
                AppData.GuardarTodo(); // 🔥 guarda después de eliminar
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
