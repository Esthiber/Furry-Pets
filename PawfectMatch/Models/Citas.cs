using PawfectMatch.Models.Servicios;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models
{
    public class Citas
    {
        [Key]
        public int CitasID { get; set; }

        [Required]
        public int PersonasID { get; set; }
        [ForeignKey(nameof(PersonasID))]
        public Personas? Persona { get; set; }

        [Required]
        public int MascotasPersonasID { get; set; }
        [ForeignKey(nameof(MascotasPersonasID))]
        public MascotasPersonas? MascotaPersona { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public TimeSpan Hora { get; set; }

        [StringLength(500)]
        public string? Motivo { get; set; }

        [Required]
        public int EstadosCitasID { get; set; }
        [ForeignKey(nameof(EstadosCitasID))]
        public EstadosCitas? EstadoCita { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}