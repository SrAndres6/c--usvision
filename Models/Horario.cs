public class Horario
{
    public int Id { get; set; }
    public string Dias { get; set; }          // Ejemplo: "Lunes, Miércoles, Viernes"
    public TimeSpan HoraInicio { get; set; }  // Ejemplo: 08:00
    public string TipoDuracion { get; set; }  // Ejemplo: "2 horas", "Semanal"
}
