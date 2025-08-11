using Domain.Contracts;
using Domain.Models;
using Persistence.Data.Context;

namespace Persistence.Repository
{
    public class UnitOfWork(ApplicationDbContext _dbContext) : IUnitOfWork
    {
     private readonly Dictionary<string, object> _Repositories = [];
       private readonly Lazy<IDepartmentRepository> _DepartmentRepository  =new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_dbContext));

        public IDepartmentRepository DepartmentRepository => _DepartmentRepository.Value;
        public IGenericRepository<T> GenericRepository<T>() where T : BaseEntity
        {
            var TypeName = typeof(T).Name;


            if (_Repositories.TryGetValue(TypeName, out object? value))
                return (IGenericRepository<T>)value;

            else
            {
                //CreateObject
                var Repo = new GenericRepository<T>(_dbContext);

                //stored In Dictionary

                _Repositories[TypeName] = Repo;

                return Repo;

            }

        }
      

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
          return await _dbContext.SaveChangesAsync(cancellationToken)>0?true:false;
        }
    }
}
