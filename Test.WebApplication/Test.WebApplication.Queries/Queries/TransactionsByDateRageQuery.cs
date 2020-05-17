using System;
using System.Collections.Generic;
using MediatR;
using Test.WebApplication.Common.Dtos;

namespace Test.WebApplication.Queries.Queries
{
    public class TransactionsByDateRageQuery : IRequest<IReadOnlyCollection<TransactionDto>>
    {
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }

    }
}
