using Budget.Web.Mvc.Models.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Budget.Web.Mvc.Models.CategoryVm
{
    public class CategoryItemVm
    {
        public Guid Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Тип")]
        public BootstrapSelectVm Type { get; set; }

        [Display(Name = "Родитель")]
        public BootstrapSelectVm ParrentCategory { get; set; }
    }
}