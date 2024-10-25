using System.ComponentModel.DataAnnotations;

namespace MicroservicioCuentaMovimiento.Models
{
    public class Cuenta
    {
        [Key]
        public int NumeroCuenta { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public decimal SaldoInicial { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public int ClienteId { get; set; }

        public ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    }
}