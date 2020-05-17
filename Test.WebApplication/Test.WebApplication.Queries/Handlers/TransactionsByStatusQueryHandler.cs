using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Test.WebApplication.Common.Dtos;
using Test.WebApplication.Queries.Queries;
using Test.WebApplication.UnitOfWork.Interfaces.UnitOfWorks;

namespace Test.WebApplication.Queries.Handlers
{
    public class TransactionsByStatusQueryHandler : IRequestHandler<TransactionsByStatusQuery, IReadOnlyCollection<TransactionDto>>
    {
        private readonly IUnitOfTest _unitOfTest;

        public TransactionsByStatusQueryHandler(IUnitOfTest unitOfTest)
        {
            _unitOfTest = unitOfTest;
        }

        public async Task<IReadOnlyCollection<TransactionDto>> Handle(TransactionsByStatusQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfTest.TransactionRepository.GetAllTransactionsByStatusValueAsync(request.TransactionStatus);
        }
    }
}
