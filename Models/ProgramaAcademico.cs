namespace proyecto_c_.Models
{
    public class ProgramaAcademico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // 🔥 Nueva propiedad
        public int Duracion { get; set; } // Número de semestres
    }
}
