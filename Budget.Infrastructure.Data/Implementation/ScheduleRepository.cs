using Budget.Domain.Core.Entity;
using Budget.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Budget.Infrastructure.Data.Implementation
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(BudgetDbContext db) : base(db)
        {
        }

        public override IEnumerable<Schedule> GetAll()
        {
            return _db.Schedules;
        }

        public override Schedule GetById(Guid Id)
        {
            return _db.Schedules.Find(Id);
        }

        public override void Insert(Schedule entity)
        {
            _db.Schedules.Add(entity);
        }

        public override void Remove(Guid entityId)
        {
            Schedule entity = _db.Schedules.Find(entityId);
            _db.Schedules.Remove(entity);
        }
    }
}
