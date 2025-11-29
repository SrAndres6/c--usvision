using System;

namespace proyecto_c_.Models
{
    public class Matricula
    {
        public int Id { get; set; }

        public int IdEstudiante { get; set; }
        public int IdPrograma { get; set; }

        public decimal Valor { get; set; }
        public string Periodo { get; set; }
        public DateTime FechaMatricula { get; set; }
    }
}
