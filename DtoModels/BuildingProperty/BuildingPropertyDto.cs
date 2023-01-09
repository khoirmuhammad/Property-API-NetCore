using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyAPI.DtoModels.BuildingProperty
{
    public class BuildingPropertyDto
    {
        public int Id { get; set; }
        public int SellRent { get; set; }
        public string Name {get; set;} = string.Empty;
        public string PropertyType {get; set;} = string.Empty;
        public int Bhk {get; set;}
        public string FurnitureType {get; set;} = string.Empty;
        public double Price {get; set;}
        public double BuiltArea {get; set;}
        public double? CarpetArea {get; set;}
        public string Address {get; set;} = string.Empty;
        public string Address2 {get; set;} = string.Empty;
        public string City {get; set;} = string.Empty;
        public string Country {get; set;} = string.Empty;
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
        public DateTime PostedOn {get; set;}
        public string PostedBy {get; set;} = string.Empty;
    }
}