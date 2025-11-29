using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using proyecto_c_.Models;
using proyecto_c_.Data;
using System.Linq;

namespace proyecto_c_.Controllers
{
    public class HorariosController : Controller
    {
        public IActionResult Index()
        {
            return View(AppData.Horarios);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Horario horario)
        {
            horario.Id = AppData.Horarios.Count + 1;
            AppData.Horarios.Add(horario);
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

            var horario = AppData.Horarios.FirstOrDefault(h => h.Id == id);
            if (horario == null) return NotFound();
            return View(horario);
        }

        [HttpPost]
        public IActionResult Editar(Horario horario)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede editar.";
                return RedirectToAction("Index", "Home");
            }

            var original = AppData.Horarios.FirstOrDefault(h => h.Id == horario.Id);
            if (original != null)
            {
                original.Dias = horario.Dias;
                original.HoraInicio = horario.HoraInicio;
                original.TipoDuracion = horario.TipoDuracion;

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

            var horario = AppData.Horarios.FirstOrDefault(h => h.Id == id);
            if (horario == null) return NotFound();
            return View(horario);
        }

        [HttpPost]
        public IActionResult EliminarConfirmado(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
            {
                TempData["Error"] = "Acceso denegado: solo el administrador puede eliminar.";
                return RedirectToAction("Index", "Home");
            }

            var horario = AppData.Horarios.FirstOrDefault(h => h.Id == id);
            if (horario != null)
            {
                AppData.Horarios.Remove(horario);
                AppData.GuardarTodo(); // 🔥 guarda después de eliminar
            }

            return RedirectToAction("Index");
        }
    }
}
