using System.Collections.Generic;
using Test.WebApplication.Common.Dtos;

namespace Test.WebApplication.UnitOfWork.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        void CreateTransactions(List<TransactionDto> transactions);
    }
}
