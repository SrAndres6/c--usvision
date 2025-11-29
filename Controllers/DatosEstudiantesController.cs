using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using proyecto_c_.Models;
using proyecto_c_.Data;
using System.Linq;

namespace proyecto_c_.Controllers
{
    public class DatosEstudianteController : Controller
    {
        public IActionResult Index()
        {
            return View(AppData.DatosEstudiante);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(DatosEstudiante datos)
        {
            datos.Id = AppData.DatosEstudiante.Count + 1;
            AppData.DatosEstudiante.Add(datos);
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

            var datos = AppData.DatosEstudiante.FirstOrDefault(d => d.Id == id);
            if (datos == null) return NotFound();
            return View(datos);
        }

        [HttpPost]
        public IActionResult Editar(DatosEstudiante datos)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede editar.";
                return RedirectToAction("Index", "Home");
            }

            var original = AppData.DatosEstudiante.FirstOrDefault(d => d.Id == datos.Id);
            if (original != null)
            {
                original.Nombre = datos.Nombre;
                original.IdDocumentoEstudiante = datos.IdDocumentoEstudiante;
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

            var datos = AppData.DatosEstudiante.FirstOrDefault(d => d.Id == id);
            if (datos == null) return NotFound();
            return View(datos);
        }

        [HttpPost]
        public IActionResult EliminarConfirmado(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede eliminar.";
                return RedirectToAction("Index", "Home");
            }

            var datos = AppData.DatosEstudiante.FirstOrDefault(d => d.Id == id);
            if (datos != null)
            {
                AppData.DatosEstudiante.Remove(datos);
                AppData.GuardarTodo(); // 🔥 guarda después de eliminar
            }

            return RedirectToAction("Index");
        }
    }
}
