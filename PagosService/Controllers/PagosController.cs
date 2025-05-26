using Microsoft.AspNetCore.Mvc;
using PagosService.Application.Interfaces;
using PagosService.Domain.Entities;

namespace PagosService.Controllers;

[ApiController]
[Route("api/pagos")]
public class PagosController : ControllerBase
{
    private readonly IPagoRepository _repo;

    public PagosController(IPagoRepository repo)
    {
        _repo = repo;
    }

    [HttpPost]
    public async Task<IActionResult> Procesar([FromBody] Pago pago)
    {
        if (pago.Monto <= 0 || string.IsNullOrWhiteSpace(pago.CorreoCliente))
            return BadRequest(new { error = "Datos inválidos" });

        var (success, idReserva, referencia) = await _repo.ProcesarPagoAsync(pago);

        return success
            ? Ok(new { status = "exitoso", idReserva, referenciaTransaccion = referencia })
            : StatusCode(500, new { status = "fallido" });
    }

}