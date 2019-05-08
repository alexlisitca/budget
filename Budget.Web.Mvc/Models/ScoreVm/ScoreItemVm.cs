using Budget.Web.Mvc.Models.Templates;
using System;
using System.ComponentModel.DataAnnotations;

namespace Budget.Web.Mvc.Models.ScoreVm
{
    public class ScoreItemVm
    {
        public Guid Id { get; set; }

        [Display(Name = "Номер")]
        public int OrderId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Тип")]
        public BootstrapSelectVm ScoreType { get; set; }

        [Display(Name = "Баланс")]
        public double Balance { get; set; }

        [Display(Name = "Начальный баланс")]
        public double InitBalance { get; set; }

        [Display(Name = "Задолженность")]
        public double CreditDebts { get; set; }

        [Display(Name = "Лимит")]
        public double CreditLimit { get; set; }

        [Display(Name = "Минимальный платеж")]
        public double CreditMinPayment { get; set; }

        [Display(Name = "Дата минимального платежа")]
        public DateTime? CreditPaymentDate { get; set; }
    }
}