using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Models;

namespace PropertyAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        public DbSet<City> Cities { get; set; } = default!;
        public DbSet<User> Users {get; set;} = default!;
        public DbSet<BuildingProperty> BuildingProperties {get; set;} = default!;
        public DbSet<FurnitureType> FurnitureTypes {get; set;} = default!;
        public DbSet<PropertyType> PropertyTypes {get; set;} = default!;
    }
}