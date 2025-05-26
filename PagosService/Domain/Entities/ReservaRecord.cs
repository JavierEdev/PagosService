using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PagosService.Domain.Entities
{
    [Table("reserva")]
    public class ReservaRecord
    {
        [Key]
        [Column("id_reserva")]
        public int IdReserva { get; set; }
        [Column("estado")]
        public string Estado { get; set; } = string.Empty;
    }

}
