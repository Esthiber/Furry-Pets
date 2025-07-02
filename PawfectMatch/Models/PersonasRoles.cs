using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models
{
    public class PersonasRoles
    {
        [Key]
        public int PersonasRolesID { get; set; }

        // Relación a Personas
        [Required]
        public int PersonasID { get; set; }

        [ForeignKey("PersonasID")]
        public Personas? Persona { get; set; }

        [Required]
        [StringLength(20)]
        public string Rol { get; set; } = string.Empty; // Ejemplo de valores: "Cliente", "Adoptante"

        public bool IsDeleted { get; set; } = false;
    }
}