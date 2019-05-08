using Budget.Domain.Interfaces;

namespace Budget.Services.Interfaces
{
    public interface IRepositoryContainer
    {
        IUserRepository Users { get; }
        IScoreRepository Scores { get; }
        ICategoryRepository Categories { get; }
        ITransactionRepository Transaction { get; }
    }
}
