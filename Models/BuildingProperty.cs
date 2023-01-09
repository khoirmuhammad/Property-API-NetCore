using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyAPI.Models
{
    public class BuildingProperty : BaseEntity
    {
        public int Id { get; set; }
        public int SellRent { get; set; }
        public string Name {get; set;} = string.Empty;
        public int PropertyTypeId {get; set;}
        public PropertyType PropertyType {get; set;} = new PropertyType();
        public int Bhk {get; set;}
        public int FurnitureTypeId {get; set;}
        public FurnitureType FurnitureType {get; set;} = new FurnitureType();
        public double Price {get; set;}
        public double BuiltArea {get; set;}
        public double? CarpetArea {get; set;}
        public string Address {get; set;} = string.Empty;
        public string Address2 {get; set;} = string.Empty;
        public int CityId {get; set;} // it will be automatically create relation with City Table, because this propety using convention name
        public City City {get; set;} = new City();
        public int FloorNo {get; set;}
        public int TotalFloor {get; set;}
        public bool IsReadyToMove {get; set;}
        public string MainEntrance {get; set;} = string.Empty;
        public int Security {get; set;}
        public bool Gated {get; set;}
        public int Maintenance {get; set;}
        public DateTime? PossessionOn {get; set;}
        public string Description {get; set;} = string.Empty;
        public int Age {get; set;}
        public ICollection<Photo> Photos {get; set;} = new List<Photo>();
        public DateTime PostedOn {get; set;}

        [ForeignKey("User")]
        public int PostedBy {get; set;}
        public User User {get; set;} = new User();
    }
}