using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using proyecto_c_.Models;
using proyecto_c_.Data;

namespace proyecto_c_.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioLogueado")))
            {
                return RedirectToAction("Index", "Login");
            }

            var resumen = new HomeViewModel
            {
                TotalEstudiantes = AppData.Estudiantes?.Count ?? 0,
                TotalDocentes = AppData.Docentes?.Count ?? 0,
                TotalMaterias = AppData.Materias?.Count ?? 0,
                TotalProgramas = AppData.Programas?.Count ?? 0,
                TotalHorarios = AppData.Horarios?.Count ?? 0,
                TotalMatriculas = AppData.Matriculas?.Count ?? 0,
                TotalGrupos = AppData.Grupos?.Count ?? 0,
                TotalDocumentos = AppData.DocumentosEstudiante?.Count ?? 0,
                TotalDatosEstudiante = AppData.DatosEstudiante?.Count ?? 0
            };

            return View(resumen);
        }
    }
}
