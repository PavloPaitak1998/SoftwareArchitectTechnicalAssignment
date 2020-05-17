using System.Collections.Generic;
using AutoMapper;
using Test.WebApplication.Common.Dtos;
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
    }
}
