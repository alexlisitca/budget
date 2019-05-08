using System;
using Budget.Web.Mvc.Models;

namespace Budget.Web.Mvc.ViewModel.Contract
{
    public interface IViewModelProvider
    {
        BudgetTotalVm GetBudetViewModel();

        ScoreViewModel GetScoreViewModel();
        void AddScore(ScoreViewModel vm);

        CategoriesVm GetCategoryViewModel();
        CategoryEditVm GetCategoryById(Guid Id);
        void AddCategory(CategoriesVm vm);
        void UpdateCategory(CategoryEditVm entity);
        FinancialVm GetFinancialViewModel();
    }
}