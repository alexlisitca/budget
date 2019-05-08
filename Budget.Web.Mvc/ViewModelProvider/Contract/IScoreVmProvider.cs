using System;
using Budget.Web.Mvc.Models.ScoreVm;

namespace Budget.Web.Mvc.ViewModelProvider.Contract
{
    public interface IScoreVmProvider
    {
        ScoreListVm GetViewModel(Guid? Id);
        ScoreItemVm GetById(Guid Id);
        void AddOrUpdateScore(ScoreItemVm vm);
        void RemoveScore (Guid Id);
    }
}