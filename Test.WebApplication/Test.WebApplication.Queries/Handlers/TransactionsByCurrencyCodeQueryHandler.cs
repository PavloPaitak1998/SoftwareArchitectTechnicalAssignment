﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Test.WebApplication.Common.Dtos;
using Test.WebApplication.Queries.Queries;
using Test.WebApplication.UnitOfWork.Interfaces.UnitOfWorks;

namespace Test.WebApplication.Queries.Handlers
{
    public class TransactionsByCurrencyCodeQueryHandler : IRequestHandler<TransactionsByCurrencyCodeQuery, IReadOnlyCollection<TransactionDto>>
    {
        private readonly IUnitOfTest _unitOfTest;

        public TransactionsByCurrencyCodeQueryHandler(IUnitOfTest unitOfTest)
        {
            _unitOfTest = unitOfTest;
        }

        public async Task<IReadOnlyCollection<TransactionDto>> Handle(TransactionsByCurrencyCodeQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfTest.TransactionRepository.GetAllTransactionsByCurrencyCodeAsync(request.CurrencyCode);
        }
    }
}
