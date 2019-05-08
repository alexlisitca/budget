using Budget.Web.Mvc.ViewModelProvider.Contract;
using System;
using Budget.Web.Mvc.Models.ScoreVm;
using Budget.Domain.Interfaces;
using Budget.Web.Mvc.Models.Templates;
using Budget.Domain.Core.Enums;
using Budget.Web.Mvc.Common.Extension;
using System.Linq;
using Budget.Domain.Core.Entity;

namespace Budget.Web.Mvc.ViewModelProvider.Implementation
{
    public class ScoreVmProvider : IScoreVmProvider
    {
        private readonly IScoreRepository _rep;

        public ScoreVmProvider(IScoreRepository rep)
        {
            _rep = rep;
        }

        public void AddOrUpdateScore(ScoreItemVm vm)
        {
            Score entity = (vm.Id == Guid.Empty) ? new Score() { Id = Guid.NewGuid() } : _rep.GetById(vm.Id);
            entity.Balance = vm.Balance;
            entity.CreditDebts = vm.CreditDebts;
            entity.CreditLimit = vm.CreditLimit;
            entity.CreditMinPayment = vm.CreditMinPayment;
            entity.CreditPaymentDate = vm.CreditPaymentDate;
            entity.Description = vm.Description;
            entity.InitBalance = vm.InitBalance;
            entity.Name = vm.Name;
            entity.OrderId = vm.OrderId;
            entity.ScoreType = vm.ScoreType.SelectedItem == "Credit" ? ScoreTypes.Credit : ScoreTypes.Debit;

            if (vm.Id == Guid.Empty)
                _rep.Insert(entity);
            else
                _rep.Update(entity);

            _rep.Save();
        }

        public ScoreItemVm GetById(Guid Id)
        {
            var types = ScoreTypes.Credit.ToList();

            var entity = _rep.GetById(Id);

            var vm = new ScoreItemVm();
            vm.Id = entity.Id;
            vm.Balance = entity.Balance;
            vm.CreditDebts = entity.CreditDebts;
            vm.CreditLimit = entity.CreditLimit;
            vm.CreditMinPayment = entity.CreditMinPayment;
            vm.CreditPaymentDate = entity.CreditPaymentDate;
            vm.Description = entity.Description;
            vm.InitBalance = entity.InitBalance;
            vm.Name = entity.Name;
            vm.OrderId = entity.OrderId;
            vm.ScoreType = new BootstrapSelectVm() { SourceList = types, SelectedItem = entity.ScoreType.ToString() };
            return vm;
        }

        public ScoreListVm GetViewModel(Guid? Id)
        {
            var types = ScoreTypes.Credit.ToList();

            return new ScoreListVm
            {
                NewItem = ((Id == null) || (Id == Guid.Empty)) ? new ScoreItemVm() { ScoreType = new BootstrapSelectVm() { SourceList = types, SelectedItem = ScoreTypes.Debit.ToString() } } : GetById((Guid)Id),
                AllRows = _rep.GetAll().Select(x => new ScoreItemVm
                {
                    Id = x.Id,
                    Balance = x.Balance,
                    CreditDebts = x.CreditDebts,
                    CreditLimit = x.CreditLimit,
                    CreditMinPayment = x.CreditMinPayment,
                    CreditPaymentDate = x.CreditPaymentDate,
                    Description = x.Description,
                    InitBalance = x.InitBalance,
                    Name = x.Name,
                    OrderId = x.OrderId,
                    ScoreType = new BootstrapSelectVm { SelectedItem = x.ScoreType.GetDescriptionOfEnum() }
                })
            };
        }

        public void RemoveScore(Guid Id)
        {
            _rep.Remove(Id);
            _rep.Save();
        }
    }
}