using Budget.Domain.Core.Enums;
using Budget.Domain.Core.Shared;
using System;
using System.Collections.Generic;

namespace Budget.Domain.Core.Entity
{
    public class Score : TEntity
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ScoreTypes ScoreType { get; set; }
        public double Balance { get; set; }
        public double InitBalance { get; set; }
        public double CreditDebts { get; set; }
        public double CreditLimit { get; set; }
        public double CreditMinPayment { get; set; }
        public DateTime? CreditPaymentDate { get; set; }

        public virtual List<Score> Scores { get; set; }
    }
}
