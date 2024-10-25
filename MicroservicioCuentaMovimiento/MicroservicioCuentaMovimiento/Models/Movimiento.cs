using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroservicioCuentaMovimiento.Models
{
    public class Movimiento
    {
        [Key]
        public int MovimientoId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string TipoMovimiento { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public decimal Saldo { get; set; }

        [ForeignKey("Cuenta")]
        public int NumeroCuenta { get; set; }
        public Cuenta Cuenta { get; set; }
    }
}
