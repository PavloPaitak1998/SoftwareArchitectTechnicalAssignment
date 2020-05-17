using System.Collections.Generic;
using MediatR;
using Test.WebApplication.Common.Dtos;
using Test.WebApplication.Common.Enums;

namespace Test.WebApplication.Queries.Queries
{
    public class TransactionsByCurrencyCodeQuery : IRequest<IReadOnlyCollection<TransactionDto>>
    {
        public CurrencyCode CurrencyCode { get; set; }
    }
}
