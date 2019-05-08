using System;
using System.Collections.Generic;

namespace Budget.Domain.Interfaces.Shared
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(Guid Id);
        void Insert(T entity);
        void Remove(Guid entityId);
        void Update(T entity);
        void Save();
    }
}
