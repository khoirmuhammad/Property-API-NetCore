using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyAPI.Data.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityReporistory {get;}
        IUserRepository UserRepository {get;}
        IPropertyTypeRepository PropertyTypeRepository {get;}
        IFurnitureTypeRepository FurnitureTypeRepository {get;}
        IBuildingPropertyRepository BuildingPropertyRepository {get;}
        Task<bool> SaveAsync();
    }
}