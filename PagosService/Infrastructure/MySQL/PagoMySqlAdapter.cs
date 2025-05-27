using PagosService.Application.Interfaces;
using PagosService.Domain.Entities;
using PagosService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace PagosService.Infrastructure.MySQL;

public class PagoMySqlAdapter : IPagoRepository
{
    private readonly MyDbContext _context;
    private readonly IMongoLogger _logger;

    public PagoMySqlAdapter(MyDbContext context, IMongoLogger logger)
    {
        _context = context;
        _logger = logger;
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

        if (result > 0)
        {
            await _logger.RegistrarLogAsync(
                "info",
                "Pago procesado exitosamente",
                new Dictionary<string, object>
                {
                    { "reservaId", record.IdReserva },
                    { "monto", record.Monto },
                    { "referencia", record.ReferenciaTransaccion }
                });

            return (true, record.IdReserva, record.ReferenciaTransaccion);
        }
        else
        {
            await _logger.RegistrarLogAsync(
                "error",
                "Error al procesar el pago",
                new Dictionary<string, object>
                {
                    { "reservaId", pago.IdReserva },
                    { "monto", pago.Monto }
                });

            return (false, 0, string.Empty);
        }
    }
}
