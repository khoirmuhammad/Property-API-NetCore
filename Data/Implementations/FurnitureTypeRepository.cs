using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Data.Interfaces;
using PropertyAPI.Models;

namespace PropertyAPI.Data.Implementations
{
    public class FurnitureTypeRepository : IFurnitureTypeRepository
    {
        private readonly DataContext _dataContext;
        public FurnitureTypeRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<IEnumerable<FurnitureType>> GetAsync(CancellationToken cancellationToken)
        {
            return await _dataContext.FurnitureTypes.ToListAsync(cancellationToken);
        }
    }
}