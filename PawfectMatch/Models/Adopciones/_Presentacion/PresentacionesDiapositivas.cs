using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models.Adopciones._Presentacion
{
    public class PresentacionesDiapositivas
    {
        [Key]
        public int PresentacionDiapositivaId { get; set; }

        [Required(ErrorMessage = "Presentacion Requerida.")]
        public int PresentacionId { get; set; }

        [Required(ErrorMessage ="Diapositiva Requerida.")]
        public int DiapositivaId { get; set; }

        public int Orden { get; set; }

        [ForeignKey("PresentacionId")]
        virtual public Presentaciones Presentacion { get; set; } = null!;
        [ForeignKey("DiapositivaId")]
        virtual public Diapositivas Diapositiva { get; set; } = null!;
    }
}
