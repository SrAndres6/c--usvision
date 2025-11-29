using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using proyecto_c_.Models;
using proyecto_c_.Data;
using System.Linq;

namespace proyecto_c_.Controllers
{
    public class ProgramasController : Controller
    {
        public IActionResult Index()
        {
            return View(AppData.Programas);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(ProgramaAcademico programa)
        {
            programa.Id = AppData.Programas.Count + 1;
            AppData.Programas.Add(programa);
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

            var programa = AppData.Programas.FirstOrDefault(p => p.Id == id);
            if (programa == null) return NotFound();
            return View(programa);
        }

        [HttpPost]
        public IActionResult Editar(ProgramaAcademico programa)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede editar.";
                return RedirectToAction("Index", "Home");
            }

            var original = AppData.Programas.FirstOrDefault(p => p.Id == programa.Id);
            if (original != null)
            {
                original.Nombre = programa.Nombre;
                original.Descripcion = programa.Descripcion;
                original.Duracion = programa.Duracion;

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

            var programa = AppData.Programas.FirstOrDefault(p => p.Id == id);
            if (programa == null) return NotFound();
            return View(programa);
        }

        [HttpPost]
        public IActionResult EliminarConfirmado(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede eliminar.";
                return RedirectToAction("Index", "Home");
            }

            var programa = AppData.Programas.FirstOrDefault(p => p.Id == id);
            if (programa != null)
            {
                AppData.Programas.Remove(programa);
                AppData.GuardarTodo(); // 🔥 guarda después de eliminar
            }

            return RedirectToAction("Index");
        }
    }
}
