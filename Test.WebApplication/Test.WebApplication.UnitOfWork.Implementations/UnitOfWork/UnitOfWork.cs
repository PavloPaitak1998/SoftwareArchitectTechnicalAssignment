using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WebApplication.Dal.Db;
using Test.WebApplication.UnitOfWork.Implementations.Repositories;
using Test.WebApplication.UnitOfWork.Interfaces.Repositories;
using Test.WebApplication.UnitOfWork.Interfaces.UnitOfWork;

namespace Test.WebApplication.UnitOfWork.Implementations.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestDbContext _context;
        private ITransactionRepository _transactionRepository;

        public UnitOfWork(TestDbContext context)
        {
            _context = context;
        }

        public ITransactionRepository TransactionRepository => 
            _transactionRepository ??= new TransactionRepository(_context);

        public async Task SaveChangesAsync()
        {
            var changes = _context.ChangeTracker.Entries().Count(
                p => p.State == EntityState.Modified
                  || p.State == EntityState.Deleted
                  || p.State == EntityState.Added);

            if (changes != 0)
            {
                await _context.SaveChangesAsync();
            }
        }

        #region IDisposable Support
        private bool _disposedValue = false;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }

                _disposedValue = true;
            }
        }
        #endregion
    }
}
