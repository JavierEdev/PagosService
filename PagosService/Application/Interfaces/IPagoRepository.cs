namespace PagosService.Application.Interfaces;

using PagosService.Domain.Entities;

public interface IPagoRepository
{
    Task<(bool, int, string)> ProcesarPagoAsync(Pago pago);
}