using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Test.WebApplication.Dal.Db;
using Test.WebApplication.UnitOfWork.Implementations.Repositories;
using Test.WebApplication.UnitOfWork.Interfaces.Repositories;
using Test.WebApplication.UnitOfWork.Interfaces.UnitOfWorks;

namespace Test.WebApplication.UnitOfWork.Implementations.UnitOfWorks
{
    public class UnitOfTest : IUnitOfTest
    {
        private readonly TestDbContext _context;
        private readonly IMapper _mapper;
        private ITransactionRepository _transactionRepository;

        public UnitOfTest(TestDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ITransactionRepository TransactionRepository => 
            _transactionRepository ??= new TransactionRepository(_context, _mapper);

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
