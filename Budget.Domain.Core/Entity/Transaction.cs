using Budget.Domain.Core.Enums;
using Budget.Domain.Core.Shared;
using System;

namespace Budget.Domain.Core.Entity
{
    public class Transaction : TEntity
    {
        public TransactionTypes TransactionType { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }

        public virtual Category TransactionCategory { get; set; }
        public virtual Score InScore { get; set; }
        public virtual Score OutScore { get; set; }
    }
}
