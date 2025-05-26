using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("pago")]
public class PagoRecord
{
    [Key]
    [Column("id_pago")]
    public int IdPago { get; set; }

    [Column("id_reserva")]
    public int IdReserva { get; set; }

    [Column("fecha_pago")]
    public DateTime FechaPago { get; set; }

    [Column("estado_transaccion")]
    public string EstadoTransaccion { get; set; } = string.Empty;

    [Column("monto")]
    public decimal Monto { get; set; }

    [Column("referencia_transaccion")]
    public string ReferenciaTransaccion { get; set; } = string.Empty;
}