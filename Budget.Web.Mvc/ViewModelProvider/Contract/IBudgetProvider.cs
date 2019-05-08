using Budget.Web.Mvc.Models.BudgetVm;

namespace Budget.Web.Mvc.ViewModelProvider.Contract
{
    public interface IBudgetProvider
    {
        BudgetTotalVm GetViewModel();
    }
}