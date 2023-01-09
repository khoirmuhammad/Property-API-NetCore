using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Data.Interfaces;
using PropertyAPI.Models;

namespace PropertyAPI.Data.Implementations
{
    public class BuildingPropertyRepository : IBuildingPropertyRepository
    {
        private readonly DataContext _dataContext;
        public BuildingPropertyRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public void Delete(BuildingProperty property)
        {
            _dataContext.Remove(property);
        }

        public async Task<IEnumerable<BuildingProperty>> GetAsync(CancellationToken cancellationToken)
        {
            return await _dataContext.BuildingProperties
            .Include(i => i.City)
            .Include(i => i.PropertyType)
            .Include(i => i.FurnitureType)
            .Include(i => i.User)
            .AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<BuildingProperty?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dataContext.BuildingProperties
            .Include(i => i.City)
            .Include(i => i.PropertyType)
            .Include(i => i.FurnitureType)
            .Include(i => i.User)
            .AsNoTracking().Where(w => w.Id.Equals(id)).FirstOrDefaultAsync(cancellationToken);
        }

        public async void PostAsync(BuildingProperty property, CancellationToken cancellationToken)
        {
            await _dataContext.AddAsync(property);
        }
    }
}