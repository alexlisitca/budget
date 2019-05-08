using System;
using Budget.Web.Mvc.Models.ScheduleVm;

namespace Budget.Web.Mvc.ViewModelProvider.Contract
{
    public interface IScheduleVmProvider
    {
        ScheduleListVm GetViewModel(Guid? Id);
        ScheduleItemVm GetById(Guid Id);
        void AddOrUpdateSchedule(ScheduleItemVm vm);
        void RemoveSchedule(Guid Id);
    }
}