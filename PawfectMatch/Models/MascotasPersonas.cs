using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models
{
    public class MascotasPersonas
    {
        [Key]
        public int MascotasPersonasID { get; set; }

        public int? PersonasID { get; set; } //Nullable
        [ForeignKey(nameof(PersonasID))]
        public Personas? Personas { get; set; } // Nullable

        public int? RazasID { get; set; }
        [ForeignKey("RazasID")]
        public Razas? Razas { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public DateOnly? FechaNacimiento { get; set; }

        [Required]
        public char Sexo { get; set; } = 'u'; // 'm', 'f', 'u' (masculino, femenino, desconocido)

        public bool IsDeleted { get; set; } = false;
    }
}