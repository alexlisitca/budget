using Budget.Web.Mvc.ViewModelProvider.Contract;
using System;
using Budget.Web.Mvc.Models.CategoryVm;
using Budget.Domain.Interfaces;
using Budget.Domain.Core.Entity;
using Budget.Domain.Core.Enums;
using Budget.Web.Mvc.Models.Templates;
using Budget.Web.Mvc.Common.Extension;
using System.Linq;
using System.Web.Mvc;

namespace Budget.Web.Mvc.ViewModelProvider.Implementation
{
    public class CategoryVmProvider : ICategoryVmProvider
    {
        private readonly SelectList _catTypes = null;
        private readonly SelectList _catParrent = null;

        private readonly ICategoryRepository _rep;

        public CategoryVmProvider(ICategoryRepository rep)
        {
            _rep = rep;
            _catTypes = CategoryType.Incoming.ToList();

            var values = from Category c in _rep.GetAll()
                         select new
                         {
                             Id = c.Id,
                             Name = c.Name
                         };
            _catParrent = new SelectList(values, "Id", "Name", null);
        }

        public void AddOrUpdateCategory(CategoryItemVm vm)
        {
            Category entity = vm.Id == Guid.Empty ? new Category() { Id = Guid.NewGuid() } : _rep.GetById(vm.Id);

            entity.Name = vm.Name;
            Guid parrenId = Guid.Empty;
            Guid.TryParse(vm.ParrentCategory.SelectedItem, out parrenId);
            entity.ParrentCategory = parrenId == Guid.Empty ? null : _rep.GetById(parrenId);
            entity.Type = vm.Type.SelectedItem == "Incoming" ? CategoryType.Incoming : CategoryType.Outcoming;

            if (vm.Id == Guid.Empty)
                _rep.Insert(entity);
            else
                _rep.Update(entity);
            _rep.Save();
        }

        public CategoryItemVm GetById(Guid Id)
        {
            var entity = _rep.GetById(Id);
            var item = new CategoryItemVm()
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = new BootstrapSelectVm() { SelectedItem = entity.Type.ToString(), SourceList = _catTypes },
            };

            if (entity.ParrentCategory == null)
            {
                item.ParrentCategory = new BootstrapSelectVm() { SourceList = _catParrent };
            }
            else
            {
                item.ParrentCategory = new BootstrapSelectVm() { SelectedItem = entity.ParrentCategory.Id.ToString(), SourceList = _catParrent };
            }
            return item;
        }

        public CategoryListVm GetViewModel(Guid? Id)
        {
            return new CategoryListVm()
            {
                NewItem = ((Id == null) || (Id == Guid.Empty)) ? 
                        new CategoryItemVm() { Type = new BootstrapSelectVm() { SourceList = _catTypes }, ParrentCategory = new BootstrapSelectVm { SourceList = _catParrent } } : 
                        GetById((Guid)Id),
                AllRows = _rep.GetAll().Select(x => new CategoryItemVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParrentCategory = new BootstrapSelectVm() { SourceList = _catParrent, SelectedItemText = (x.ParrentCategory != null) ? x.ParrentCategory.Name : null, SelectedItem = (x.ParrentCategory != null) ? x.ParrentCategory.Id.ToString() : null },
                    Type = new BootstrapSelectVm() { SelectedItem = x.Type.GetDescriptionOfEnum() }
                })
            };
        }

        public void RemoveCategory(Guid Id)
        {
            _rep.Remove(Id);
            _rep.Save();
        }
    }
}