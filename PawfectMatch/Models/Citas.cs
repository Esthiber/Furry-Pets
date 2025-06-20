using PawfectMatch.Models._Mascotas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models
{
    public class Citas
    {
        [Key]
        public int CitaId { get; set; }

        public int AdoptanteId { get; set; }

        public int MascotaId { get; set; }

        public DateTime FechaHora { get; set; }

        [ForeignKey("AdoptanteId")]
        public Adoptantes Adoptante { get; set; } = null!;

        [ForeignKey("MascotaId")]
        public Mascotas Mascota { get; set; } = null!;

    }
}
