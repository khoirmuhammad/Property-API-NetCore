using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Data.Interfaces;
using PropertyAPI.Models;

namespace PropertyAPI.Data.Implementations
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _dataContext;
        public CityRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public void Delete(City city)
        {
            _dataContext.Cities.Remove(city);                
        }

        public async Task<City> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var city = await _dataContext.Cities.FirstOrDefaultAsync(f => f.Id.Equals(id), cancellationToken);

            return city ?? new City();
        }

        public async Task<IEnumerable<City>> GetAsync(CancellationToken cancellationToken)
        {
            return await _dataContext.Cities.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async void PostAsync(City city, CancellationToken cancellationToken)
        {
            await _dataContext.AddAsync(city);
        }
    }
}