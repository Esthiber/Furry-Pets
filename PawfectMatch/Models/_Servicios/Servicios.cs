using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models._Servicios
{
    public class Servicios
    {
        [Key]
        public int ServicioId { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        [Range(0,100000)]
        public double Costo { get; set; }
    }
}
