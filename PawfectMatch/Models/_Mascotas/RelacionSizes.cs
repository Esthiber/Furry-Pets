using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models._Mascotas
{
    public class RelacionSizes
    {
        [Key]
        public int RelacionSizeId { get; set; }

        public string Size { get; set; } = null!;
    }
}
