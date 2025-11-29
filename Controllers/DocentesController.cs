using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using proyecto_c_.Models;
using proyecto_c_.Data;
using System.Linq;

namespace proyecto_c_.Controllers
{
    public class DocentesController : Controller
    {
        public IActionResult Index()
        {
            return View(AppData.Docentes);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Docente docente)
        {
            docente.Id = AppData.Docentes.Count + 1;
            AppData.Docentes.Add(docente);
            AppData.GuardarTodo(); // 🔥 guarda automáticamente
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede editar.";
                return RedirectToAction("Index", "Home");
            }

            var docente = AppData.Docentes.FirstOrDefault(d => d.Id == id);
            if (docente == null) return NotFound();
            return View(docente);
        }

        [HttpPost]
        public IActionResult Editar(Docente docente)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede editar.";
                return RedirectToAction("Index", "Home");
            }

            var original = AppData.Docentes.FirstOrDefault(d => d.Id == docente.Id);
            if (original != null)
            {
                original.Nombre = docente.Nombre;
                original.Apellidos = docente.Apellidos;
                original.Telefono = docente.Telefono;
                original.Correo = docente.Correo;
                original.DocumentoNacional = docente.DocumentoNacional;
                original.FechaIngreso = docente.FechaIngreso;

                AppData.GuardarTodo(); // 🔥 guarda cambios
            }
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede eliminar.";
                return RedirectToAction("Index", "Home");
            }

            var docente = AppData.Docentes.FirstOrDefault(d => d.Id == id);
            if (docente == null) return NotFound();
            return View(docente);
        }

        [HttpPost]
        public IActionResult EliminarConfirmado(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede eliminar.";
                return RedirectToAction("Index", "Home");
            }

            var docente = AppData.Docentes.FirstOrDefault(d => d.Id == id);
            if (docente != null)
            {
                AppData.Docentes.Remove(docente);
                AppData.GuardarTodo(); // 🔥 guarda después de eliminar
            }

            return RedirectToAction("Index");
        }
    }
}
