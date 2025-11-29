using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using proyecto_c_.Models;
using proyecto_c_.Data;
using System.Linq;

namespace proyecto_c_.Controllers
{
    public class DocumentosEstudianteController : Controller
    {
        public IActionResult Index()
        {
            return View(AppData.DocumentosEstudiante);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(DocumentoEstudiante documento)
        {
            documento.Id = AppData.DocumentosEstudiante.Count + 1;
            AppData.DocumentosEstudiante.Add(documento);
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

            var documento = AppData.DocumentosEstudiante.FirstOrDefault(d => d.Id == id);
            if (documento == null) return NotFound();
            return View(documento);
        }

        [HttpPost]
        public IActionResult Editar(DocumentoEstudiante documento)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede editar.";
                return RedirectToAction("Index", "Home");
            }

            var original = AppData.DocumentosEstudiante.FirstOrDefault(d => d.Id == documento.Id);
            if (original != null)
            {
                original.Nombre = documento.Nombre;
                original.Descripcion = documento.Descripcion;
                original.Ruta = documento.Ruta;

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

            var documento = AppData.DocumentosEstudiante.FirstOrDefault(d => d.Id == id);
            if (documento == null) return NotFound();
            return View(documento);
        }

        [HttpPost]
        public IActionResult EliminarConfirmado(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede eliminar.";
                return RedirectToAction("Index", "Home");
            }

            var documento = AppData.DocumentosEstudiante.FirstOrDefault(d => d.Id == id);
            if (documento != null)
            {
                AppData.DocumentosEstudiante.Remove(documento);
                AppData.GuardarTodo(); // 🔥 guarda después de eliminar
            }

            return RedirectToAction("Index");
        }
    }
}
