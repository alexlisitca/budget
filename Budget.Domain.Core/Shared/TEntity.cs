using System;
using System.ComponentModel.DataAnnotations;

namespace Budget.Domain.Core.Shared
{
    public class TEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
