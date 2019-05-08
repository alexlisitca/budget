using Budget.Web.Mvc.Models.Templates;
using Budget.Web.Mvc.Models.ScoreVm;
using System;
using System.ComponentModel.DataAnnotations;

namespace Budget.Web.Mvc.Models.ScheduleVm
{
    public class ScheduleItemVm
    {
        public Guid Id { get; set; }

        [Display(Name = "Дата")]
        public DateTime ActionDate { get; set; }

        [Display(Name = "Название")]
        public string ShortName { get; set; }

        [Display(Name = "Сумма")]
        public double Amount { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Категория")]
        public BootstrapSelectVm ActionCategory { get; set; }

        [Display(Name = "Счет")]
        public BootstrapSelectVm FromScore { get; set; }
    }
}