using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models._Mascotas
{
    public class Estados
    {
        [Key]
        public int EstadoId { get; set; }

        public string Nombre { get; set; } = null!;
    }
}
