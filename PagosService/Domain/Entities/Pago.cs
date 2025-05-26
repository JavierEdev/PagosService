namespace PagosService.Domain.Entities
{
    public class Pago
    {
        public int IdReserva { get; set; }
        public decimal Monto { get; set; }
        public string CorreoCliente { get; set; } = string.Empty;
        public string Tarjeta { get; set; } = string.Empty;
    }
}
