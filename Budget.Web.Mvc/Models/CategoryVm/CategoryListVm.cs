using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Budget.Web.Mvc.Models.CategoryVm
{
    public class CategoryListVm
    {
        public IEnumerable<CategoryItemVm> AllRows { get; set; }

        [UIHint("CategoryItemView")]
        public CategoryItemVm NewItem { get; set; }
    }
}