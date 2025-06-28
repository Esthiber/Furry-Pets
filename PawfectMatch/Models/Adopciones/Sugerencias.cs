using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models.Adopciones
{
    public class Sugerencias
    {
        [Key]
        public int SugerenciaId { get; set; }

        [Required(ErrorMessage ="El correo es requerido.")]
        public string UserMail { get; set; } = null!;

        [Required(ErrorMessage = "Es requerido una descripcion.")]
        public string Descripcion { get; set; } = null!;
    }
}
