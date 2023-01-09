using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropertyAPI.Models;

namespace PropertyAPI.Data.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAsync(CancellationToken cancellationToken);
        Task<City> GetByIdAsync(int id, CancellationToken cancellationToken);
        void PostAsync(City city, CancellationToken cancellationToken);
        void Delete(City city);
    }
}