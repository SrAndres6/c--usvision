using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using proyecto_c_.Models;
using proyecto_c_.Data;
using System.Linq;

namespace proyecto_c_.Controllers
{
    public class MatriculasController : Controller
    {
        public IActionResult Index()
        {
            return View(AppData.Matriculas);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Matricula matricula)
        {
            matricula.Id = AppData.Matriculas.Count + 1;
            AppData.Matriculas.Add(matricula);
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

            var matricula = AppData.Matriculas.FirstOrDefault(m => m.Id == id);
            if (matricula == null) return NotFound();
            return View(matricula);
        }

        [HttpPost]
        public IActionResult Editar(Matricula matricula)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede editar.";
                return RedirectToAction("Index", "Home");
            }

            var original = AppData.Matriculas.FirstOrDefault(m => m.Id == matricula.Id);
            if (original != null)
            {
                original.IdEstudiante = matricula.IdEstudiante;
                original.IdPrograma = matricula.IdPrograma;
                original.Valor = matricula.Valor;
                original.Periodo = matricula.Periodo;
                original.FechaMatricula = matricula.FechaMatricula;

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

            var matricula = AppData.Matriculas.FirstOrDefault(m => m.Id == id);
            if (matricula == null) return NotFound();
            return View(matricula);
        }

        [HttpPost]
        public IActionResult EliminarConfirmado(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede eliminar.";
                return RedirectToAction("Index", "Home");
            }

            var matricula = AppData.Matriculas.FirstOrDefault(m => m.Id == id);
            if (matricula != null)
            {
                AppData.Matriculas.Remove(matricula);
                AppData.GuardarTodo(); // 🔥 guarda después de eliminar
            }

            return RedirectToAction("Index");
        }
    }
}
