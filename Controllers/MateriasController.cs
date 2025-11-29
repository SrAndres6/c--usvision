using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using proyecto_c_.Models;
using proyecto_c_.Data;
using System.Linq;

namespace proyecto_c_.Controllers
{
    public class MateriasController : Controller
    {
        public IActionResult Index()
        {
            return View(AppData.Materias);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Materia materia)
        {
            materia.Id = AppData.Materias.Count + 1;
            AppData.Materias.Add(materia);
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

            var materia = AppData.Materias.FirstOrDefault(m => m.Id == id);
            if (materia == null) return NotFound();
            return View(materia);
        }

        [HttpPost]
        public IActionResult Editar(Materia materia)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede editar.";
                return RedirectToAction("Index", "Home");
            }

            var original = AppData.Materias.FirstOrDefault(m => m.Id == materia.Id);
            if (original != null)
            {
                original.Nombre = materia.Nombre;
                original.Descripcion = materia.Descripcion;
                original.Creditos = materia.Creditos;

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

            var materia = AppData.Materias.FirstOrDefault(m => m.Id == id);
            if (materia == null) return NotFound();
            return View(materia);
        }

        [HttpPost]
        public IActionResult EliminarConfirmado(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede eliminar.";
                return RedirectToAction("Index", "Home");
            }

            var materia = AppData.Materias.FirstOrDefault(m => m.Id == id);
            if (materia != null)
            {
                AppData.Materias.Remove(materia);
                AppData.GuardarTodo(); // 🔥 guarda después de eliminar
            }

            return RedirectToAction("Index");
        }
    }
}
