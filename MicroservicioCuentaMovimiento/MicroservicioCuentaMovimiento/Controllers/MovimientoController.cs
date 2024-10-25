using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioCuentaMovimiento.Data;
using MicroservicioCuentaMovimiento.Models;
using System.Threading.Tasks;

namespace MicroservicioCuentaMovimiento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovimientoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimiento>>> GetMovimientos()
        {
            return await _context.Movimientos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movimiento>> GetMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound("Movimiento no encontrado.");
            }
            return movimiento;
        }

        [HttpPost]
        public async Task<ActionResult<Movimiento>> RegistrarMovimiento(Movimiento movimiento)
        {
            var cuenta = await _context.Cuentas.FindAsync(movimiento.NumeroCuenta);
            if (cuenta == null)
            {
                return NotFound("Cuenta no encontrada.");
            }

            // Validar saldo para retiros (F3)
            if (movimiento.TipoMovimiento.ToLower() == "retiro" && cuenta.SaldoInicial < movimiento.Valor)
            {
                return BadRequest("Saldo no disponible.");
            }

            // Actualizar saldo de la cuenta (F2)
            if (movimiento.TipoMovimiento.ToLower() == "retiro")
            {
                cuenta.SaldoInicial -= movimiento.Valor;
            }
            else if (movimiento.TipoMovimiento.ToLower() == "deposito")
            {
                cuenta.SaldoInicial += movimiento.Valor;
            }

            movimiento.Saldo = cuenta.SaldoInicial;
            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovimiento", new { id = movimiento.MovimientoId }, movimiento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound("Movimiento no encontrado.");
            }

            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}