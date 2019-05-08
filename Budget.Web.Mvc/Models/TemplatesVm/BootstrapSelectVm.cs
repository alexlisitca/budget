using System.Collections.Generic;
using System.Web.Mvc;

namespace Budget.Web.Mvc.Models.Templates
{
    public class BootstrapSelectVm
    {
        public IEnumerable<SelectListItem> SourceList { get; set; }
        public string SelectedItem { get; set; }
        public string SelectedItemText { get; set; }
    }
}