using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyAPI.Models
{
    [Table("Photos")]
    public class Photo : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string PhotoUrl {get; set;} = string.Empty;
        public bool IsPrimary {get; set;} = false;
        public int BuildingPropertyId {get; set;}
        public BuildingProperty BuildingProperty {get; set;} = default!;
    }
}