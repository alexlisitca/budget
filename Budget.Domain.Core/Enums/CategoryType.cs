using System.ComponentModel.DataAnnotations;

namespace Budget.Domain.Core.Enums
{
    public enum CategoryType
    {
        [Display(Description = "Доход")]
        Incoming,
        [Display(Description = "Расход")]
        Outcoming
    }
}
