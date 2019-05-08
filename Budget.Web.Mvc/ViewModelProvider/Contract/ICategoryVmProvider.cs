using System;
using Budget.Web.Mvc.Models.CategoryVm;

namespace Budget.Web.Mvc.ViewModelProvider.Contract
{
    public interface ICategoryVmProvider
    {
        CategoryListVm GetViewModel(Guid? Id);
        CategoryItemVm GetById(Guid Id);
        void AddOrUpdateCategory(CategoryItemVm vm);
        void RemoveCategory(Guid Id);
    }
}