using Budget.Domain.Interfaces;
using Budget.Web.Mvc.Models.BudgetVm;
using Budget.Web.Mvc.Models.ScheduleVm;
using Budget.Web.Mvc.Models.Templates;
using Budget.Web.Mvc.ViewModelProvider.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Budget.Web.Mvc.ViewModelProvider.Implementation
{
    public class BudgetProvider : IBudgetProvider
    {
        private readonly IScoreRepository _scoreProvider;
        private readonly IScheduleRepository _scheduleProvider;

        public BudgetProvider(IScoreRepository scoreProvider, IScheduleRepository scheduleProvider)
        {
            _scoreProvider = scoreProvider;
            _scheduleProvider = scheduleProvider;
        }

        public BudgetTotalVm GetViewModel()
        {
            BudgetTotalVm vm = new BudgetTotalVm();

            vm.Scores = new Models.ScoreVm.ScoreListVm();
            vm.Scores.AllRows = _scoreProvider.GetAll().Select(x => new Models.ScoreVm.ScoreItemVm()
            {
               Balance = x.Balance,
               Name = x.Name,
               CreditDebts = x.CreditDebts,
               CreditLimit = x.CreditLimit,
               OrderId = x.OrderId,
               ScoreType = new BootstrapSelectVm() { SelectedItemText = x.ScoreType.ToString() }
            });
            vm.Schedule = new Dictionary<DateTime, List<ScheduleItemVm>>();

            var collection = _scheduleProvider.GetAll().Select(x => new ScheduleItemVm()
            {
                ActionDate = x.ActionDate,
                Amount = x.Amount,
                Description = x.Description,
                ShortName = x.ShortName
            }).ToList();

            var datesCollection = collection.Select(x => x.ActionDate).ToList();
            List<DateTime> dates = datesCollection.Distinct().OrderBy(x=>x.ToShortDateString()).ToList();

            foreach (DateTime date in dates)
            {
                List<ScheduleItemVm> list  = collection.Where(x => x.ActionDate.ToShortDateString() == date.ToShortDateString()).ToList();
                vm.Schedule.Add(date, list);
            }

            return vm;
        }
    }
}