using Budget.Domain.Core.Entity;
using Budget.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Budget.Infrastructure.Data.Implementation
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BudgetDbContext db) : base(db)
        {
        }

        public override IEnumerable<Category> GetAll()
        {
            return _db.Categories;
        }

        public override Category GetById(Guid Id)
        {
            return _db.Categories.Find(Id);
        }

        public override void Insert(Category entity)
        {
            _db.Categories.Add(entity);
        }

        public override void Remove(Guid entityId)
        {
            Category entity = _db.Categories.Find(entityId);
            _db.Categories.Remove(entity);
        }
    }
}
