using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models.Adopciones._Presentacion
{
    public class Presentaciones
    {
        [Key]
        public int PresentacionId { get; set; }

        [Required(ErrorMessage = "El Titulo es requerido.")]
        public string Titulo { get; set; } = null!;

        public string? Descripcion { get; set; }

        public bool EsActiva { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [InverseProperty("Presentacion")]
        public virtual ICollection<PresentacionesDiapositivas> PresentacionesDiapositivas { get; set; } = null!;
    }
}
