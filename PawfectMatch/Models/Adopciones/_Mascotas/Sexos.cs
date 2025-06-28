using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models.Adopciones._Mascotas
{
    public class Sexos
    {
        [Key]
        public int SexoId { get; set; }

        public string Nombre { get; set; } = null!;
    }
}
