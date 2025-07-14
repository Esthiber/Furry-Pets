using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PawfectMatch.Urls;

namespace PawfectMatch.Models.Adopciones
{
    public class MascotasAdopcion
    {
        [Key]
        public int MascotasAdopcionID { get; set; }

        [Required]
        public int RazasID { get; set; }
        [ForeignKey(nameof(RazasID))]
        public Razas? Razas { get; set; }

        [Required]
        public int RelacionSizeID { get; set; }
        [ForeignKey("RelacionSizeID")]
        public RelacionSize? RelacionSize { get; set; }

        public int? Tamanio { get; set; } // X de pulgadas de alto (puede ser null)

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public DateOnly? FechaNacimiento { get; set; }

        [StringLength(255)]
        public string? Descripcion { get; set; }

        public string? FotoURL { get; set; }

        [Required]
        public int EstadoID { get; set; }
        [ForeignKey("EstadoID")]
        public Estados? Estado { get; set; }

        [Required]
        public char Sexo { get; set; } = 'u'; // 'm', 'f', 'u'

        public bool IsDeleted { get; set; } = false;
    }
}