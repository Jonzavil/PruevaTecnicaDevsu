using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroservicioClientePersona.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [ForeignKey("Persona")]
        public int PersonaId { get; set; }

        public Persona Persona { get; set; }
        [Required]
        public string Contraseña { get; set; } = string.Empty;

        [Required]
        public bool Estado { get; set; } = true;
    }
}
