using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropertyAPI.Data.Interfaces;

namespace PropertyAPI.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        public UnitOfWork(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public ICityRepository CityReporistory => new CityRepository(_dataContext);
        public IUserRepository UserRepository => new UserRepository(_dataContext);
        public IBuildingPropertyRepository BuildingPropertyRepository => new BuildingPropertyRepository(_dataContext);
        public IPropertyTypeRepository PropertyTypeRepository => new PropertyTypeRepository(_dataContext);

        public IFurnitureTypeRepository FurnitureTypeRepository => new FurnitureTypeRepository(_dataContext);

        public async Task<bool> SaveAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}