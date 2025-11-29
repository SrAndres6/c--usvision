namespace proyecto_c_.Models
{
    public class Docente
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }   // ✅ agregado

        public string Telefono { get; set; }    // ✅ agregado

        public string Correo { get; set; }      // ✅ agregado

        public string DocumentoNacional { get; set; } // ✅ agregado

        public DateTime FechaIngreso { get; set; }   // ✅ agregado

        public string Especialidad { get; set; }
    }
}
