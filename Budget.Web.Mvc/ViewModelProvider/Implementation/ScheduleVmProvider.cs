using Budget.Web.Mvc.ViewModelProvider.Contract;
using System;
using Budget.Domain.Interfaces;
using Budget.Web.Mvc.Models.ScheduleVm;
using Budget.Domain.Core.Entity;
using Budget.Web.Mvc.Models.Templates;
using System.Linq;
using System.Web.Mvc;

namespace Budget.Web.Mvc.ViewModelProvider.Implementation
{
    public class ScheduleVmProvider : IScheduleVmProvider
    {
        private readonly IScheduleRepository _schRep;
        private readonly IScoreRepository _scRep;
        private readonly ICategoryRepository _catRep;

        private readonly SelectList _catParrent = null;
        private readonly SelectList _scoresList = null;

        public ScheduleVmProvider(IScheduleRepository schRep, IScoreRepository scRep, ICategoryRepository catRep)
        {
            _schRep = schRep;
            _scRep = scRep;
            _catRep = catRep;

            var values = from Category c in _catRep.GetAll()
                         select new
                         {
                             Id = c.Id,
                             Name = c.Name
                         };
            _catParrent = new SelectList(values, "Id", "Name", null);

            values = from Score c in _scRep.GetAll()
                     select new
                     {
                         Id = c.Id,
                         Name = c.Name
                     };
            _scoresList = new SelectList(values, "Id", "Name", null);
        }

        public void AddOrUpdateSchedule(ScheduleItemVm vm)
        {
            Schedule entity = vm.Id == Guid.Empty ? new Schedule() { Id = Guid.NewGuid() } : _schRep.GetById(vm.Id);

            entity.ActionCategory = _catRep.GetById(Guid.Parse(vm.ActionCategory.SelectedItem));

            entity.ActionDate = vm.ActionDate;
            entity.Amount = vm.Amount;
            entity.Description = vm.Description;
            entity.ShortName = vm.ShortName;
            entity.FromScore = _scRep.GetById(Guid.Parse(vm.FromScore.SelectedItem));
            
            if (vm.Id == Guid.Empty)
                _schRep.Insert(entity);
            else
                _schRep.Update(entity);
            _schRep.Save();
        }

        public ScheduleItemVm GetById(Guid Id)
        {
            ScheduleItemVm vm = new ScheduleItemVm();

            var entity = _schRep.GetById(Id);

            vm.ActionCategory = new BootstrapSelectVm()
            {
                SourceList = _catParrent,
                SelectedItem = entity.ActionCategory.Id.ToString(),
                SelectedItemText = entity.ActionCategory.Name
            };

            vm.ActionDate = entity.ActionDate;
            vm.Amount = entity.Amount;
            vm.Description = entity.Description;
            vm.Id = entity.Id;
            vm.ShortName = entity.ShortName;

            vm.FromScore = new BootstrapSelectVm()
            {
                SourceList = _scoresList,
                SelectedItem = entity.FromScore.Id.ToString(),
                SelectedItemText = entity.FromScore.Name
            };

            return vm;
        }

        public ScheduleListVm GetViewModel(Guid? Id)
        {
            ScheduleListVm vm = new ScheduleListVm()
            {
                NewItem = ((Id == null) || (Id == Guid.Empty)) ?
                        new ScheduleItemVm() { ActionCategory = new BootstrapSelectVm() { SourceList = _catParrent }, FromScore = new BootstrapSelectVm { SourceList = _scoresList } } :
                        GetById((Guid)Id),
                AllRows = _schRep.GetAll().Select(x => new ScheduleItemVm()
                {
                    ActionCategory = new BootstrapSelectVm() { SelectedItemText = x.ActionCategory.Name, SelectedItem = x.ActionCategory.Id.ToString() },
                    ActionDate = x.ActionDate,
                    Id = x.Id,
                    Amount = x.Amount,
                    Description = x.Description,
                    ShortName = x.ShortName,
                    FromScore = new BootstrapSelectVm() { SelectedItemText = x.FromScore.Name, SelectedItem = x.FromScore.Id.ToString() }
                }).OrderBy(x => x.ActionDate)
            };

            return vm;
        }

        public void RemoveSchedule(Guid Id)
        {
            _schRep.Remove(Id);
            _schRep.Save();
        }
    }
}