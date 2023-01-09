using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropertyAPI.Models;

namespace PropertyAPI.Data.Interfaces
{
    public interface IFurnitureTypeRepository
    {
        Task<IEnumerable<FurnitureType>> GetAsync(CancellationToken cancellationToken);
    }
}