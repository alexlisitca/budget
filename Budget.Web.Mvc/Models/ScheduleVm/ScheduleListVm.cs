using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Budget.Web.Mvc.Models.ScheduleVm
{
    public class ScheduleListVm
    {
        public IEnumerable<ScheduleItemVm> AllRows { get; set; }

        [UIHint("ScheduleItemView")]
        public ScheduleItemVm NewItem { get; set; }
    }
}