using Microsoft.AspNetCore.Mvc;
using proyecto_c_.Models;
using System.Text.Json;

namespace proyecto_c_.Controllers
{
    public class LoginController : Controller
    {
        private static List<Usuario> usuarios = new();

        static LoginController()
        {
            // Cargar usuarios desde JSON si existe
            if (System.IO.File.Exists("Data/usuarios.json"))
            {
                var json = System.IO.File.ReadAllText("Data/usuarios.json");
                usuarios = JsonSerializer.Deserialize<List<Usuario>>(json) ?? new List<Usuario>();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Usuario model)
        {
            var user = usuarios.FirstOrDefault(u =>
                u.NombreUsuario == model.NombreUsuario && u.Clave == model.Clave);

            if (user != null)
            {
                HttpContext.Session.SetString("UsuarioLogueado", user.NombreUsuario);
                HttpContext.Session.SetString("Rol", user.Rol);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Usuario o clave incorrectos";
            return View(model);
        }

        // ✅ Acción GET para mostrar formulario de registro
        public IActionResult Registro()
        {
            return View();
        }

        // ✅ Acción POST para guardar nuevo usuario
        [HttpPost]
        public IActionResult Registro(Usuario nuevoUsuario)
        {
            nuevoUsuario.Rol = "Usuario"; // por defecto no es admin
            usuarios.Add(nuevoUsuario);

            // Guardar en JSON
            var json = JsonSerializer.Serialize(usuarios);
            System.IO.File.WriteAllText("Data/usuarios.json", json);

            return RedirectToAction("Index"); // vuelve al login
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
