using PagosService.Application.Interfaces;
using PagosService.Domain.Entities;
using PagosService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace PagosService.Infrastructure.MySQL;

public class PagoMySqlAdapter : IPagoRepository
{
    private readonly MyDbContext _context;

    public PagoMySqlAdapter(MyDbContext context)
    {
        _context = context;
    }

    public async Task<(bool, int, string)> ProcesarPagoAsync(Pago pago)
    {
        var referencia = Guid.NewGuid().ToString();

        var record = new PagoRecord
        {
            IdReserva = pago.IdReserva,
            FechaPago = DateTime.UtcNow,
            EstadoTransaccion = "exitoso",
            Monto = pago.Monto,
            ReferenciaTransaccion = referencia
        };

        await _context.Pagos.AddAsync(record);

        var reserva = await _context.Reservas.FirstOrDefaultAsync(r => r.IdReserva == pago.IdReserva);
        if (reserva != null)
        {
            reserva.Estado = "pagada";
        }

        var result = await _context.SaveChangesAsync();

        return result > 0
            ? (true, record.IdReserva, record.ReferenciaTransaccion)
            : (false, 0, string.Empty);
    }
}
