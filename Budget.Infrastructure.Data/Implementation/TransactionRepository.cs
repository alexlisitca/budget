using Budget.Domain.Core.Entity;
using Budget.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Budget.Infrastructure.Data.Implementation
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BudgetDbContext db) : base(db)
        {
        }

        public override IEnumerable<Transaction> GetAll()
        {
            return _db.Transactions;
        }

        public override Transaction GetById(Guid Id)
        {
            return _db.Transactions.Find(Id);
        }

        public override void Insert(Transaction entity)
        {
            _db.Transactions.Add(entity);
        }

        public override void Remove(Guid entityId)
        {
            Transaction entity = _db.Transactions.Find(entityId);
            _db.Transactions.Remove(entity);
        }
    }
}
