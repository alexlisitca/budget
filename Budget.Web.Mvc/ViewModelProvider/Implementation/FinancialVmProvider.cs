using Budget.Web.Mvc.ViewModelProvider.Contract;
using System;
using Budget.Web.Mvc.Models.FinancialVm;
using Budget.Domain.Interfaces;
using System.Linq;
using Budget.Domain.Core.Entity;
using Budget.Domain.Core.Enums;
using Budget.Web.Mvc.Models.Templates;
using System.Web.Mvc;
using Budget.Web.Mvc.Common.Extension;

namespace Budget.Web.Mvc.ViewModelProvider.Implementation
{
    public class FinancialVmProvider : IFinancialVmProvider
    {
        private readonly ITransactionRepository _rep;
        private readonly IScoreRepository _scRep;
        private readonly ICategoryRepository _catRep;

        private readonly SelectList _catList = null;
        private readonly SelectList _scoreList = null;

        public FinancialVmProvider(ITransactionRepository rep, IScoreRepository scoreRep, ICategoryRepository catRep)
        {
            _rep = rep;
            _scRep = scoreRep;
            _catRep = catRep;

            var values = from Category c in _catRep.GetAll()
                         select new
                         {
                             Id = c.Id,
                             Name = c.Name
                         };
            _catList = new SelectList(values, "Id", "Name", null);

            values = from Score c in _scRep.GetAll()
                         select new
                         {
                             Id = c.Id,
                             Name = c.Name
                         };
            _scoreList = new SelectList(values, "Id", "Name", null);
        }

        public void AddOrUpdateTransaction(FinancialItemlVm vm)
        {
            Transaction entity = vm.Id == Guid.Empty ? new Transaction() { Id = Guid.NewGuid() } : _rep.GetById(vm.Id);

            double lastAmount = entity.Amount;
            entity.Amount = vm.Amount;
            entity.Description = vm.Description;
            entity.InScore = vm.InScore.SelectedItem == null ? null : _scRep.GetById(Guid.Parse(vm.InScore.SelectedItem));
            entity.OutScore = vm.OutScore.SelectedItem == null ? null : _scRep.GetById(Guid.Parse(vm.OutScore.SelectedItem));
            entity.TransactionDate = vm.TransactionDate;
            entity.TransactionCategory = vm.TransactionCategory.SelectedItem == null ? null : _catRep.GetById(Guid.Parse(vm.TransactionCategory.SelectedItem));

            if (vm.TransactionType.SelectedItem == "Incoming")
            {
                entity.TransactionType = TransactionTypes.Incoming;

                entity.InScore.Balance -= lastAmount;
                entity.InScore.Balance += vm.Amount;
            }
            if (vm.TransactionType.SelectedItem == "Outcoming")
            {
                entity.TransactionType = TransactionTypes.Outcoming;
                entity.OutScore.Balance += lastAmount;
                entity.OutScore.Balance -= vm.Amount;
            }
            if (vm.TransactionType.SelectedItem == "Transfer")
            {
                entity.TransactionType = TransactionTypes.Transfer;

                entity.OutScore.Balance += lastAmount;
                entity.InScore.Balance -= lastAmount;

                entity.OutScore.Balance -= vm.Amount;
                entity.InScore.Balance += vm.Amount;
            }

            if (vm.Id == Guid.Empty)
                _rep.Insert(entity);
            else
                _rep.Update(entity);
            _rep.Save();
        }

        public FinancialItemlVm GetById(Guid Id)
        {
            var entity = _rep.GetById(Id);
            var item = new FinancialItemlVm()
            {
                Id = entity.Id,
                Amount = entity.Amount,
                Description = entity.Description,
                InScore = new BootstrapSelectVm() { SourceList = _scoreList, SelectedItem = (entity.InScore == null) ? null : entity.InScore.Id.ToString(), SelectedItemText = (entity.InScore == null) ? null : entity.InScore.Name },
                OutScore = new BootstrapSelectVm() { SourceList = _scoreList, SelectedItem = (entity.OutScore == null) ? null : entity.OutScore.Id.ToString(), SelectedItemText = (entity.OutScore == null) ? null : entity.OutScore.Name },
                TransactionCategory = new BootstrapSelectVm() { SourceList = _catList, SelectedItem = (entity.TransactionCategory == null) ? null : entity.TransactionCategory.Id.ToString(), SelectedItemText = (entity.TransactionCategory == null) ? null : entity.TransactionCategory.Name },
                TransactionDate = entity.TransactionDate,
                TransactionType = new BootstrapSelectVm() { SourceList = TransactionTypes.Incoming.ToList(), SelectedItem = entity.TransactionType.ToString(), SelectedItemText = entity.TransactionType.GetDescriptionOfEnum() }
            };
            return item;
        }

        public FinancialListVm GetViewModel(Guid? Id)
        {
            var vm = new FinancialListVm()
            {
                AllRows = _rep.GetAll().Select(x => new FinancialItemlVm()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Description = x.Description,
                    InScore = new BootstrapSelectVm() { SourceList = _scoreList, SelectedItem = (x.InScore == null) ? null : x.InScore.Id.ToString(), SelectedItemText = (x.InScore == null) ? null : x.InScore.Name },
                    OutScore = new BootstrapSelectVm() { SourceList = _scoreList, SelectedItem = (x.OutScore == null) ? null : x.OutScore.Id.ToString(), SelectedItemText = (x.OutScore == null) ? null : x.OutScore.Name },
                    TransactionCategory = new BootstrapSelectVm() { SourceList = _catList, SelectedItem = (x.TransactionCategory == null) ? null : x.TransactionCategory.Id.ToString(), SelectedItemText = (x.TransactionCategory == null) ? null : x.TransactionCategory.Name },
                    TransactionDate = x.TransactionDate,
                    TransactionType = new BootstrapSelectVm() { SourceList = TransactionTypes.Incoming.ToList(), SelectedItem = x.TransactionType.ToString(), SelectedItemText = x.TransactionType.GetDescriptionOfEnum() }
                })
            };

            if ((Id == null) || (Id == Guid.Empty))
            {
                vm.NewItem = new FinancialItemlVm()
                {
                    TransactionDate = DateTime.Now,
                    InScore = new BootstrapSelectVm() { SourceList = _scoreList },
                    OutScore = new BootstrapSelectVm() { SourceList = _scoreList },
                    TransactionCategory = new BootstrapSelectVm() { SourceList = _catList },
                    TransactionType = new BootstrapSelectVm() { SourceList = TransactionTypes.Incoming.ToList() }

                };
            }
            else
            {
                vm.NewItem = GetById((Guid)Id);
            }

            return vm;
        }

        public void RemoveTransaction(Guid Id)
        {
            var entity = _rep.GetById(Id);

            if (entity.TransactionType.ToString() == "Incoming")
            {
                entity.InScore.Balance -= entity.Amount;
            }
            if (entity.TransactionType.ToString() == "Outcoming")
            {
                entity.OutScore.Balance += entity.Amount;
            }
            if (entity.TransactionType.ToString() == "Transfer")
            {
                entity.OutScore.Balance += entity.Amount;
                entity.InScore.Balance -= entity.Amount;
            }
            _rep.Save();

            _rep.Remove(Id);
            _rep.Save();
        }
    }
}