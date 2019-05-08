using System;
using Budget.Web.Mvc.ViewModel.Contract;
using Budget.Web.Mvc.Models;
using Budget.Services.Interfaces;
using System.Linq;
using Budget.Domain.Core.Enums;
using Budget.Domain.Core.Entity;
using System.Web.Mvc;

namespace Budget.Web.Mvc.ViewModel.Implementation
{
    public class ViewModelProvider : IViewModelProvider
    {
        private readonly IRepositoryContainer _rep;

        public ViewModelProvider(IRepositoryContainer rep)
        {
            _rep = rep;
        }

        public void AddCategory(CategoriesVm vm)
        {
            if (!string.IsNullOrEmpty(vm.NewCategory.Name))
            {
                Category newCategory = new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = vm.NewCategory.Name,
                    Type = vm.NewCategory.Type
                };
                newCategory.ParrentCategory = vm.ParrentCategory.SelectedItem == null ? null : _rep.Categories.GetById(Guid.Parse(vm.ParrentCategory.SelectedItem));

                _rep.Categories.Insert(newCategory);
                _rep.Categories.Save();
            }
        }

        public void AddScore(ScoreViewModel vm)
        {
            
            if (!string.IsNullOrEmpty(vm.NewScore.Name))
            {
                Score newScore = new Score()
                {
                    Id = Guid.NewGuid(),
                    ScoreType = vm.NewScore.ScoreType,
                    CreditDebts = vm.NewScore.CreditDebts,
                    Balance = vm.NewScore.Balance,
                    Description = vm.NewScore.Description,
                    Name = vm.NewScore.Name
                };
                _rep.Scores.Insert(newScore);
                _rep.Scores.Save();
            }
        }

        public BudgetTotalVm GetBudetViewModel()
        {
            BudgetTotalVm vm = new BudgetTotalVm();
            vm.Scores = _rep.Scores.GetAll();
            vm.TotalSum = vm.Scores.Sum(x => x.Balance);
            vm.TotalSumWithoutCredit = vm.Scores.Where(x => x.ScoreType != ScoreTypes.Credit).Sum(x => x.Balance);
            vm.TotalCreditDebts = vm.Scores.Where(x => x.ScoreType == ScoreTypes.Credit).Sum(x => x.CreditDebts);
            vm.MinPayment = vm.Scores.Where(x => x.ScoreType == ScoreTypes.Credit).FirstOrDefault()?.CreditMinPayment;
            vm.IsMinPaymentSucces = false;
            vm.PaymentDate = vm.Scores.Where(x => x.ScoreType == ScoreTypes.Credit).FirstOrDefault()?.CreditPaymentDate;

            return vm;
        }

        public CategoryEditVm GetCategoryById(Guid Id)
        {
            Category entity = _rep.Categories.GetById(Id);

            CategoryEditVm vm = new CategoryEditVm()
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type
            };

            vm.ParrentCategory = new BootstrapSelectVm()
            {
                SourceList = _rep.Categories.GetAll().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }),
                SelectedItem = entity.ParrentCategory == null ? null : entity.ParrentCategory.Id.ToString()
            };
            return vm;
        }

        public CategoriesVm GetCategoryViewModel()
        {
            CategoriesVm vm = new CategoriesVm()
            {
                NewCategory = new Category(),
                Categories = _rep.Categories.GetAll(),
            };
            vm.ParrentCategory = new BootstrapSelectVm()
            {
                SourceList = vm.Categories.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }),
                SelectedItem = null
            };
            return vm;
        }

        public FinancialVm GetFinancialViewModel()
        {
            FinancialVm vm = new FinancialVm();
            vm.AllRows = _rep.Transaction.GetAll();

            return vm;
        }

        public ScoreViewModel GetScoreViewModel()
        {
            ScoreViewModel vm = new ScoreViewModel()
            {
                NewScore = new Score(),
                Scores = _rep.Scores.GetAll()
            };
            return vm;
        }

        public void UpdateCategory(CategoryEditVm entity)
        {
            var sEntity = _rep.Categories.GetById(entity.Id);
            sEntity.Name = entity.Name;
            sEntity.ParrentCategory = _rep.Categories.GetById(Guid.Parse(entity.ParrentCategory.SelectedItem));

            _rep.Categories.Update(sEntity);
        }
    }
}