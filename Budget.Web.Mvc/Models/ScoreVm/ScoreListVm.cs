using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Budget.Web.Mvc.Models.ScoreVm
{
    public class ScoreListVm
    {
        public IEnumerable<ScoreItemVm> AllRows { get; set; }

        [UIHint("ScoreItemView")]
        public ScoreItemVm NewItem { get; set; }
    }
}