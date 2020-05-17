using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Test.WebApplication.Common.Dtos;
using Test.WebApplication.Common.Enums;
using Test.WebApplication.Dal.Db;
using Test.WebApplication.Dal.Entities;
using Test.WebApplication.UnitOfWork.Interfaces.Repositories;

namespace Test.WebApplication.UnitOfWork.Implementations.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TestDbContext _context;
        private readonly IMapper _mapper;

        public TransactionRepository(TestDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void CreateTransactions(List<TransactionDto> transactions)
        {
            var entities = _mapper.Map<List<Transaction>>(transactions);

            _context.Transactions.AddRange(entities);
        }

        public async Task<IReadOnlyCollection<TransactionDto>> GetAllTransactionsByCurrencyCodeAsync(CurrencyCode currencyCode)
        {
            return await _context.Transactions
                .AsNoTracking()
                .Where(x => x.CurrencyCodeId == currencyCode)
                .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<TransactionDto>> GetAllTransactionsByStatusValueAsync(TransactionStatusValue statusValue)
        {
            return await _context.Transactions
                .AsNoTracking()
                .Where(x => x.TransactionStatusId == statusValue)
                .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<TransactionDto>> GetAllTransactionsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _context.Transactions
                .AsNoTracking()
                .Where(x => x.TransactionDate >= fromDate && x.TransactionDate <= toDate)
                .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
