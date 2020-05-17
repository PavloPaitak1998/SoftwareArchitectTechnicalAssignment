using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.WebApplication.Common.Dtos;
using Test.WebApplication.Common.Enums;

namespace Test.WebApplication.UnitOfWork.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        void CreateTransactions(List<TransactionDto> transactions);

        Task<IReadOnlyCollection<TransactionDto>> GetAllTransactionsByCurrencyCodeAsync(CurrencyCode currencyCode);

        Task<IReadOnlyCollection<TransactionDto>> GetAllTransactionsByStatusValueAsync(TransactionStatusValue statusValue);

        Task<IReadOnlyCollection<TransactionDto>> GetAllTransactionsByDateRangeAsync(DateTime fromDate, DateTime toDate);
    }
}
