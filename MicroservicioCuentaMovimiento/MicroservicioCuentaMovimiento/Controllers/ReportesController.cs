using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioCuentaMovimiento.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MicroservicioCuentaMovimiento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReporteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetEstadoCuenta(int clienteId, DateTime fechaInicio, DateTime fechaFin)
        {
            var cuentas = await _context.Cuentas
                .Include(c => c.Movimientos)
                .Where(c => c.ClienteId == clienteId)
                .ToListAsync();

            if (cuentas == null || cuentas.Count == 0)
            {
                return NotFound("Cuentas no encontradas para el cliente.");
            }

            var movimientosFiltrados = cuentas.SelectMany(c => c.Movimientos)
                .Where(m => m.Fecha >= fechaInicio && m.Fecha <= fechaFin)
                .ToList();

            var reporte = new
            {
                ClienteId = clienteId,
                Cuentas = cuentas.Select(c => new
                {
                    NumeroCuenta = c.NumeroCuenta,
                    SaldoInicial = c.SaldoInicial,
                    Movimientos = movimientosFiltrados.Select(m => new
                    {
                        m.Fecha,
                        m.Valor,
                        m.TipoMovimiento
                    })
                })
            };

            return Ok(reporte);
        }
    }
}