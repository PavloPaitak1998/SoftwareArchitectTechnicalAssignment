using System;
using System.Threading.Tasks;
using Test.WebApplication.UnitOfWork.Interfaces.Repositories;

namespace Test.WebApplication.UnitOfWork.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITransactionRepository TransactionRepository { get; }
        Task SaveChangesAsync();
    }
}
