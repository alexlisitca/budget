using Budget.Web.Mvc.Models.Templates;
using System;
using System.ComponentModel.DataAnnotations;

namespace Budget.Web.Mvc.Models.FinancialVm
{
    public class FinancialItemlVm
    {
        public Guid Id { get; set; }

        [Display(Name = "Тип")]
        public BootstrapSelectVm TransactionType { get; set; }

        [Display(Name = "Сумма")]
        public double Amount { get; set; }

        [Display(Name = "Коментарий")]
        public string Description { get; set; }

        [Display(Name = "Дата")]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Категория")]
        public BootstrapSelectVm TransactionCategory { get; set; }

        [Display(Name = "Из")]
        public BootstrapSelectVm OutScore { get; set; }

        [Display(Name = "В")]
        public BootstrapSelectVm InScore { get; set; }
    }
}