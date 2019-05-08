using System.ComponentModel.DataAnnotations;

namespace Budget.Domain.Core.Enums
{
    public enum ScoreTypes
    {
        [Display(Description = "Дебетовый")]
        Debit,
        [Display(Description = "Кредитный")]
        Credit
    }
}
