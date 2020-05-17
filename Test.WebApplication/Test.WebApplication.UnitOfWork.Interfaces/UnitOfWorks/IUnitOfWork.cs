using System;
using System.Threading.Tasks;
using Test.WebApplication.UnitOfWork.Interfaces.Repositories;

namespace Test.WebApplication.UnitOfWork.Interfaces.UnitOfWorks
{
    public interface IUnitOfTest : IDisposable
    {
        ITransactionRepository TransactionRepository { get; }
        Task SaveChangesAsync();
    }
}
