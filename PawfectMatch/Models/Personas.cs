using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models
{
    public class Personas
    {
        [Key]
        public int PersonasID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [StringLength(200)]
        public string Direccion { get; set; } = string.Empty;

        [Required]
        public char Sexo { get; set; } = 'u'; // 'm', 'f', 'u' (masculino, femenino, desconocido)

        [StringLength(50)]
        public string Identificacion { get; set; } = string.Empty;

        public DateOnly? FechaNacimiento { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(100)]
        public string Nacionalidad { get; set; } = string.Empty;

        [StringLength(50)]
        public string EstadoCivil { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        // Navegación hacia otros modelos
        public ICollection<PersonasRoles>? Roles { get; set; }
        public ICollection<AdoptantesDetalles>? AdoptantesDetalles { get; set; }
        public ICollection<MascotasPersonas>? Mascotas { get; set; }
        public ICollection<Facturas>? Facturas { get; set; }
        public ICollection<SolicitudesAdopciones>? SolicitudesAdopciones { get; set; }
        public ICollection<SeguimientoMascotas>? Seguimientos { get; set; }
        public ICollection<Citas>? Citas { get; set; }
        public ICollection<HistoriasClinicas>? HistoriasClinicas { get; set; }
    }
}