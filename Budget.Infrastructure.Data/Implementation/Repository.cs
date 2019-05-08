using Budget.Domain.Interfaces.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Budget.Infrastructure.Data.Implementation
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected BudgetDbContext _db;

        public Repository(BudgetDbContext db)
        {
            _db = db;
        }

        public abstract IEnumerable<T> GetAll();

        public abstract T GetById(Guid Id);

        public abstract void Insert(T entity);

        public abstract void Remove(Guid entityId);

        public virtual void Save()
        {
            _db.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}
