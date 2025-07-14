using PawfectMatch.Models.Servicios;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models
{
    public class HistoriasClinicas
    {
        [Key]
        public int HistoriasClinicaID { get; set; }

        [Required]
        public int MascotasPersonasID { get; set; }
        [ForeignKey(nameof(MascotasPersonasID))]
        public MascotasPersonas? MascotaPersona { get; set; }

        [Required]
        public int PersonasID { get; set; }
        [ForeignKey(nameof(PersonasID))]
        public Personas? Veterinario { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(500)]
        public string DescripcionVisita { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Diagnostico { get; set; }

        [StringLength(500)]
        public string? Tratamiento { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}