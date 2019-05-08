using Budget.Domain.Interfaces;
using Budget.Services.Interfaces;

namespace Budget.Infrastructure.Business
{
    public class RepositoryContainer : IRepositoryContainer
    {
        private IUserRepository _users;
        private IScoreRepository _scores;
        private ICategoryRepository _categ;
        private ITransactionRepository _trans;

        public RepositoryContainer(IUserRepository users, IScoreRepository scores, ICategoryRepository categ, ITransactionRepository trans)
        {
            _categ = categ;
            _users = users;
            _scores = scores;
            _trans = trans;
        }

        public ICategoryRepository Categories { get { return _categ; } }
        public IUserRepository Users { get { return _users; } }
        public IScoreRepository Scores { get { return _scores; } }
        public ITransactionRepository Transaction { get { return _trans; } }
    }
}
