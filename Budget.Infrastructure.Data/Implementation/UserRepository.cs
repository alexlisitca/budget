using Budget.Domain.Core.Entity;
using Budget.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Budget.Infrastructure.Data.Implementation
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(BudgetDbContext db) : base(db)
        {
        }

        public override IEnumerable<ApplicationUser> GetAll()
        {
            return _db.AppUsers;
        }

        public override ApplicationUser GetById(Guid Id)
        {
            return _db.AppUsers.Find(Id);
        }

        public override void Insert(ApplicationUser entity)
        {
            _db.AppUsers.Add(entity);
        }

        public override void Remove(Guid entityId)
        {
            ApplicationUser entity = _db.AppUsers.Find(entityId);
            _db.AppUsers.Remove(entity);
        }
    }
}
