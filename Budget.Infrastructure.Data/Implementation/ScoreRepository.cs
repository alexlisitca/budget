using Budget.Domain.Core.Entity;
using Budget.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Budget.Infrastructure.Data.Implementation
{
    public class ScoreRepository : Repository<Score>, IScoreRepository
    {
        public ScoreRepository(BudgetDbContext db) : base(db)
        {
        }

        public override IEnumerable<Score> GetAll()
        {
            return _db.Scores;
        }

        public override Score GetById(Guid Id)
        {
            return _db.Scores.Find(Id);
        }

        public override void Insert(Score entity)
        {
            _db.Scores.Add(entity);
        }

        public override void Remove(Guid entityId)
        {
            Score entity = _db.Scores.Find(entityId);
            _db.Scores.Remove(entity);
        }
    }
}
