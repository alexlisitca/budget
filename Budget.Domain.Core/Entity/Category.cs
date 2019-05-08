using Budget.Domain.Core.Enums;
using Budget.Domain.Core.Shared;

namespace Budget.Domain.Core.Entity
{
    public class Category : TEntity
    {
        public string Name { get; set; }
        public CategoryType Type { get; set; }

        public virtual Category ParrentCategory { get; set; }
    }
}
