using System.Collections.Generic;
using MediatR;
using Test.WebApplication.Common.Dtos;
using Test.WebApplication.Common.Enums;

namespace Test.WebApplication.Queries.Queries
{
    public class TransactionsByStatusQuery : IRequest<IReadOnlyCollection<TransactionDto>>
    {
        public TransactionStatusValue TransactionStatus { get; set; }
    }
}
