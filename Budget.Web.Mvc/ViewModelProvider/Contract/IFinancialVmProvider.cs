using Budget.Web.Mvc.Models.FinancialVm;
using System;

namespace Budget.Web.Mvc.ViewModelProvider.Contract
{
    public interface IFinancialVmProvider
    {
        FinancialListVm GetViewModel(Guid? Id);
        FinancialItemlVm GetById(Guid Id);
        void AddOrUpdateTransaction(FinancialItemlVm vm);
        void RemoveTransaction(Guid Id);
    }
}