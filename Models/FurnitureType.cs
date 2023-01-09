using System.ComponentModel.DataAnnotations;

namespace PropertyAPI.Models
{
    public class FurnitureType : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}