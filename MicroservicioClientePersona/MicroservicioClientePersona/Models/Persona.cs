using System.ComponentModel.DataAnnotations;

namespace MicroservicioClientePersona.Models
{
    public class Persona
    {
        [Key]
        public int PersonaId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
        public string Identificacion { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Telefono { get; set; }
    }
}