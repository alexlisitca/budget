using Budget.Domain.Core.Shared;

namespace Budget.Domain.Core.Entity
{
    public class ApplicationUser : TEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
