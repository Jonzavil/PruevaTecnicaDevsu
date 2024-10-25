using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioCuentaMovimiento.Data;
using MicroservicioCuentaMovimiento.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioCuentaMovimiento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CuentaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{numeroCuenta:int}")]
        public async Task<ActionResult<Cuenta>> GetCuenta(int numeroCuenta)
        {
            var cuenta = await _context.Cuentas
                .Include(c => c.Movimientos)
                .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);

            if (cuenta == null)
            {
                return NotFound();
            }

            return cuenta;
        }

        [HttpPost]
        public async Task<ActionResult<Cuenta>> CreateCuenta(Cuenta cuenta)
        {
            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCuenta), new { numeroCuenta = cuenta.NumeroCuenta }, cuenta);
        }

        [HttpPut("{numeroCuenta:int}")]
        public async Task<IActionResult> UpdateCuenta(int numeroCuenta, Cuenta cuenta)
        {
            if (numeroCuenta != cuenta.NumeroCuenta)
            {
                return BadRequest();
            }

            _context.Entry(cuenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(numeroCuenta))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{numeroCuenta:int}")]
        public async Task<IActionResult> DeleteCuenta(int numeroCuenta)
        {
            var cuenta = await _context.Cuentas.FindAsync(numeroCuenta);
            if (cuenta == null)
            {
                return NotFound();
            }

            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuentaExists(int numeroCuenta)
        {
            return _context.Cuentas.Any(e => e.NumeroCuenta == numeroCuenta);
        }
    }
}