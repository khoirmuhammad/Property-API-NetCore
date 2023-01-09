using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Data.Interfaces;
using PropertyAPI.Models;

namespace PropertyAPI.Data.Implementations
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly DataContext _dataContext;
        public PropertyTypeRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<IEnumerable<PropertyType>> GetAsync(CancellationToken cancellationToken)
        {
            return await _dataContext.PropertyTypes.ToListAsync(cancellationToken);
        }
    }
}