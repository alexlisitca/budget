using Budget.Domain.Core.Shared;
using System;

namespace Budget.Domain.Core.Entity
{
    public class Schedule: TEntity
    {
        public DateTime ActionDate { get; set; }
        public string ShortName { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

        public virtual Category ActionCategory { get; set; }
        public virtual Score FromScore { get; set; }
    }
}
