using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models.Adopciones._Mascotas
{
    public class RelacionSizes
    {
        [Key]
        public int RelacionSizeId { get; set; }

        public string Size { get; set; } = null!;
    }
}
