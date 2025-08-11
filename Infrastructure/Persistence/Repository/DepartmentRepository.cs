using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Context;

namespace Persistence.Repository
{
    public class DepartmentRepository(ApplicationDbContext _dbContext) : GenericRepository<Department>(_dbContext), IDepartmentRepository
    {
        public async Task<bool> AddAsync(Department entity, CancellationToken cancellation = default)
        {
            var ExsitEntity = await _dbContext.Set<Department>().FirstOrDefaultAsync(E => E.Name.ToLower() == entity.Name.ToLower(), cancellation);


            if (ExsitEntity is not null && !ExsitEntity.IsDeleted)
            {
                return false; // Entity with the same name already exists
            } // Entity with the same name already exists
            if(ExsitEntity is not null && ExsitEntity.IsDeleted)
            {
                // If the entity exists but is marked as deleted, restore it
                ExsitEntity.IsDeleted = false;
                _dbContext.Set<Department>().Update(ExsitEntity);
                return true; // Entity restored successfully
            }

            var result=  await _dbContext.Set<Department>().AddAsync(entity,cancellation) ;
                if (result == null)
                    return false; // Entity not added successfully

                return result.State == EntityState.Added? true : false;

        }

        public async Task<bool> Update(Department entity, CancellationToken cancellation = default)
        {
            var ExsitEntity = await _dbContext.Set<Department>().FirstOrDefaultAsync(E => E.Name.ToLower() == entity.Name.ToLower(), cancellation);
          
            
            if (ExsitEntity is not null && !ExsitEntity.IsDeleted ) { 
                    return false; // Entity with the same name already exists
            }
            entity.IsDeleted = false; // Ensure the entity is not marked as deleted
                                      // Update the entity
            var result = _dbContext.Set<Department>().Update(entity);
              return result.State == EntityState.Modified ? true : false;
        }
    }
}
