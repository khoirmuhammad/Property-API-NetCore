using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropertyAPI.Models;

namespace PropertyAPI.Data.Interfaces
{
    public interface IBuildingPropertyRepository
    {
        Task<IEnumerable<BuildingProperty>> GetAsync(CancellationToken cancellationToken);
        Task<BuildingProperty?> GetByIdAsync(int id, CancellationToken cancellationToken);
        void PostAsync(BuildingProperty property, CancellationToken cancellationToken);
        void Delete(BuildingProperty property);
    }
}