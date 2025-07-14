using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models.Adopciones
{
    public class AdoptantesDetalles
    {
        [Key]
        public int AdoptantesDetallesID { get; set; }

        [Required]
        public int PersonasID { get; set; }
        [ForeignKey("PersonasID")]
        public Personas? Persona { get; set; }

        public int? TipoViviendasID { get; set; }
        [ForeignKey("TipoViviendasID")]
        public TipoViviendas? TipoVivienda { get; set; }

        public bool ViveEnViviendaAlquilada { get; set; }

        public bool TieneJardin { get; set; }

        [StringLength(255)]
        public string? NotasJardin { get; set; }

        public bool TieneOtrasMascotas { get; set; }

        [StringLength(255)]
        public string? NotasOtrasMascotas { get; set; }

        public int? HorasAusentes { get; set; }

        [StringLength(200)]
        public string? RazonAdopcion { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}