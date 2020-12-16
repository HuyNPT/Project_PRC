using ASS_PRC.Data.Context;
using ASS_PRC.Data.Repository;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASS_PRC.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly PRCContext _context;
        public UnitOfWork(PRCContext context)
        {
            _context = context;
        }
        Dictionary<Type, object> reposotories = new Dictionary<Type, object>();
        public IGenericRepository<T> Repository<T>()
            where T : class
        {
            Type type = typeof(T);
            if (!reposotories.TryGetValue(type, out object value))
            {
                var genericRepos = new GenericRepository<T>(_context);
                reposotories.Add(type, genericRepos);
                return genericRepos;
            }
            return value as GenericRepository<T>;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Commit()
        {
            OnBeforeSaving();
            return _context.SaveChanges();
        }

        private void OnBeforeSaving()
        {
            var now = DateTime.Now;
            bool isOwned = false;

            foreach (var item in _context.ChangeTracker.Entries())
            {
                switch (item.State)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                        {
                            PropertyInfo entityInstance = item.Entity.GetType().GetProperty("ModifyDate");
                            if (entityInstance != null)
                            {
                                entityInstance.SetValue(item.Entity, now);
                            }
                            break;
                        }
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        {
                            PropertyInfo entityInstance = item.Entity.GetType().GetProperty("CreateDate");
                            if (entityInstance != null)
                            {
                                entityInstance.SetValue(item.Entity, now);
                            }
                            break;
                        }
                    default:
                        break;
                }

            }
        }


        public Task<int> CommitAsync() => _context.SaveChangesAsync();
    }
}
