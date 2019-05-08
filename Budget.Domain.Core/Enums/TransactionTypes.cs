using System.ComponentModel.DataAnnotations;

namespace Budget.Domain.Core.Enums
{
    public enum TransactionTypes
    {
        [Display(Description = "Доход")]
        Incoming,
        [Display(Description = "Расход")]
        Outcoming,
        [Display(Description = "Перевод")]
        Transfer
    }
}
