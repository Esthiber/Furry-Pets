using PawfectMatch.Models.Adopciones._Adoptantes;
using PawfectMatch.Models.Adopciones._Mascotas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models.Adopciones._Solicitudes
{
    public class HistorialAdopciones
    {
        [Key]
        public int HistorialAdopcioneId { get; set; }

        public int AdoptanteId { get; set; }

        public int MascotaId { get; set; }

        public DateTime FechaAdopcion { get; set; }

        public string? Notas { get; set; }

        [ForeignKey("AdoptanteId")]
        public Adoptantes Adoptante { get; set; } = null!;

        [ForeignKey("MascotaId")]
        public Mascotas Mascota { get; set; } = null!;

    }
}
