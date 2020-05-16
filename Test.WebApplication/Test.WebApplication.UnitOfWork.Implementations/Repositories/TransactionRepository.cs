using Test.WebApplication.Dal.Db;
using Test.WebApplication.UnitOfWork.Interfaces.Repositories;

namespace Test.WebApplication.UnitOfWork.Implementations.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TestDbContext _context;

        public TransactionRepository(TestDbContext context)
        {
            _context = context;
        }
    }
}
