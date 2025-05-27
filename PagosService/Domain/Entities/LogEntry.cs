namespace PagosService.Domain.Entities
{
    public class LogEntry
    {
        public string Nivel { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public Dictionary<string, object>? Contexto { get; set; }
    }
}