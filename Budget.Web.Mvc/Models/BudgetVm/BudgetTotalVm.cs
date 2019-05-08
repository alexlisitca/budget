using Budget.Web.Mvc.Models.ScheduleVm;
using Budget.Web.Mvc.Models.ScoreVm;
using System;
using System.Collections.Generic;

namespace Budget.Web.Mvc.Models.BudgetVm
{
    public class BudgetTotalVm
    {
        public Dictionary<DateTime, List<ScheduleItemVm>> Schedule { get; internal set; }
        public ScoreListVm Scores { get; internal set; }
    }
}