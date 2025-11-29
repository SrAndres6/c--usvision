using proyecto_c_.Models;
using System.Collections.Generic;
using System.IO;

namespace proyecto_c_.Data
{
    public static class AppData
    {
        public static List<Estudiante> Estudiantes { get; set; }
        public static List<Docente> Docentes { get; set; }
        public static List<Materia> Materias { get; set; }
        public static List<ProgramaAcademico> Programas { get; set; }
        public static List<Horario> Horarios { get; set; }
        public static List<Matricula> Matriculas { get; set; }
        public static List<Grupo> Grupos { get; set; }
        public static List<DocumentoEstudiante> DocumentosEstudiante { get; set; }
        public static List<DatosEstudiante> DatosEstudiante { get; set; }

        static string basePath = "DataStorage";

        static AppData()
        {
            Directory.CreateDirectory(basePath);

            Estudiantes = JsonStorage.Cargar<Estudiante>($"{basePath}/estudiantes.json");
            Docentes = JsonStorage.Cargar<Docente>($"{basePath}/docentes.json");
            Materias = JsonStorage.Cargar<Materia>($"{basePath}/materias.json");
            Programas = JsonStorage.Cargar<ProgramaAcademico>($"{basePath}/programas.json");
            Horarios = JsonStorage.Cargar<Horario>($"{basePath}/horarios.json");
            Matriculas = JsonStorage.Cargar<Matricula>($"{basePath}/matriculas.json");
            Grupos = JsonStorage.Cargar<Grupo>($"{basePath}/grupos.json");
            DocumentosEstudiante = JsonStorage.Cargar<DocumentoEstudiante>($"{basePath}/documentos.json");
            DatosEstudiante = JsonStorage.Cargar<DatosEstudiante>($"{basePath}/datos.json");
        }

        public static void GuardarTodo()
        {
            JsonStorage.Guardar($"{basePath}/estudiantes.json", Estudiantes);
            JsonStorage.Guardar($"{basePath}/docentes.json", Docentes);
            JsonStorage.Guardar($"{basePath}/materias.json", Materias);
            JsonStorage.Guardar($"{basePath}/programas.json", Programas);
            JsonStorage.Guardar($"{basePath}/horarios.json", Horarios);
            JsonStorage.Guardar($"{basePath}/matriculas.json", Matriculas);
            JsonStorage.Guardar($"{basePath}/grupos.json", Grupos);
            JsonStorage.Guardar($"{basePath}/documentos.json", DocumentosEstudiante);
            JsonStorage.Guardar($"{basePath}/datos.json", DatosEstudiante);
        }
    }
}
