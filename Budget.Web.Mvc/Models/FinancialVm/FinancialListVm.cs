using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Budget.Web.Mvc.Models.FinancialVm
{
    public class FinancialListVm
    {
        public IEnumerable<FinancialItemlVm> AllRows { get; set; }

        [UIHint("FinancialItemView")]
        public FinancialItemlVm NewItem { get; set; }
    }
}