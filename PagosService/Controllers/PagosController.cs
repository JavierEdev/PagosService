using Microsoft.AspNetCore.Mvc;
using PagosService.Application.Interfaces;
using PagosService.Domain.Entities;

namespace PagosService.Controllers;

[ApiController]
[Route("api/pagos")]
public class PagosController : ControllerBase
{
    private readonly IPagoRepository _repo;
    private readonly IMongoLogger _logger;

    public PagosController(IPagoRepository repo, IMongoLogger logger)
    {
        _repo = repo;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Procesar([FromBody] Pago pago)
    {
        if (pago.Monto <= 0 || string.IsNullOrWhiteSpace(pago.CorreoCliente))
        {
            await _logger.RegistrarLogAsync(
                "warning",
                "Solicitud de pago inválida",
                new Dictionary<string, object>
                {
                    { "monto", pago.Monto },
                    { "correoCliente", pago.CorreoCliente },
                    { "estado", "rechazado" }
                });

            return BadRequest(new { error = "Datos inválidos" });
        }

        var (success, idReserva, referencia) = await _repo.ProcesarPagoAsync(pago);

        await _logger.RegistrarLogAsync(
            "info",
            "Resultado del procesamiento de pago",
            new Dictionary<string, object>
            {
                { "input", new Dictionary<string, object> {
                    { "idReserva", pago.IdReserva },
                    { "correoCliente", pago.CorreoCliente },
                    { "monto", pago.Monto }
                }},
                { "output", new Dictionary<string, object> {
                    { "success", success },
                    { "idReserva", idReserva },
                    { "referencia", referencia }
                }}
            });

        return success
            ? Ok(new { status = "exitoso", idReserva, referenciaTransaccion = referencia })
            : StatusCode(500, new { status = "fallido" });
    }
}
