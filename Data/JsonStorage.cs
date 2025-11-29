using System.IO;
using System.Text.Json;

namespace proyecto_c_.Data
{
    public static class JsonStorage
    {
        public static void Guardar<T>(string ruta, List<T> datos)
        {
            var json = JsonSerializer.Serialize(datos);
            File.WriteAllText(ruta, json);
        }

        public static List<T> Cargar<T>(string ruta)
        {
            if (!File.Exists(ruta)) return new List<T>();
            var json = File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
    }
}
