using System;
using System.Collections.Generic;
using MediatR;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Test.WebApplication.Commands.CommandResults;
using Test.WebApplication.Commands.Commands;
using Test.WebApplication.Commands.FileDeserializer;
using Test.WebApplication.Commands.Models;
using Test.WebApplication.Common.Dtos;
using Test.WebApplication.Common.Enums;
using Test.WebApplication.UnitOfWork.Interfaces.UnitOfWorks;

namespace Test.WebApplication.Commands.Handlers
{
    public class FileUploadCommandHandler : IRequestHandler<FileUploadCommand, FileUploadResult>
    {
        private readonly ILogger _log;
        private readonly IMapper _mapper;
        private readonly IUnitOfTest _unitOfTest;
        private readonly IFileDeserializerFactory _fileDeserializerFactory;

        public FileUploadCommandHandler(
              IMapper mapper
            , IUnitOfTest unitOfTest
            , ILogger<FileUploadCommandHandler> log
            , IFileDeserializerFactory fileDeserializerFactory
        )
        {
            _log = log;
            _mapper = mapper;
            _unitOfTest = unitOfTest;
            _fileDeserializerFactory = fileDeserializerFactory;
        }

        public async Task<FileUploadResult> Handle(FileUploadCommand request, CancellationToken cancellationToken)
        {
            var fileType = Path.GetExtension(request.File.FileName).ToFileType();

            var fileDeserializer = _fileDeserializerFactory.GetFileDeserializer(fileType);

            var records = fileDeserializer.DeserializeFileContent<SerializableTransaction>(request.File.OpenReadStream()).ToList();

            if (!TryValidateTransactions(records, out var invalidTransactions))
            {
                return new FileUploadResult(ResultStatus.Failed, invalidTransactions.ToList());
            }

            var transactions = _mapper.Map<List<TransactionDto>>(records);

            _unitOfTest.TransactionRepository.CreateTransactions(transactions);
            await _unitOfTest.SaveChangesAsync();

            return new FileUploadResult {Status = ResultStatus.Success};
        }

        private bool TryValidateTransactions(List<SerializableTransaction> transactions, out List<InvalidTransaction> invalidTransactions)
        {
            invalidTransactions = _mapper.Map<List<InvalidTransaction>>(transactions);

            invalidTransactions.RemoveAll(TryValidateTransaction);

            LogInvalidTransactions(invalidTransactions);

            return invalidTransactions.Count == 0;
        }

        private bool TryValidateTransaction(InvalidTransaction transaction)
        {
            var errorList = new List<string>();

            var type = transaction.GetType();
            var props = new List<PropertyInfo>(type.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                if (prop.Name == nameof(InvalidTransaction.ErrorMessage))
                {
                    continue;
                }

                var propValue = prop.GetValue(transaction) as string;

                if (string.IsNullOrEmpty(propValue))
                {
                    errorList.Add($"The {prop.Name} can not be empty.");
                }
            }

            if (errorList.Count > 0)
            {
                transaction.ErrorMessage = string.Join(" ", errorList);
                return false;
            }

            return true;
        }

        private void LogInvalidTransactions(IEnumerable<InvalidTransaction> invalidTransactions)
        {
            foreach (var it in invalidTransactions)
            {
                var invalidTransaction =  new
                {
                      it.TransactionIdentificator
                    , it.Amount
                    , it.CurrencyCode
                    , it.TransactionDate
                    , it.Status
                    , it.ErrorMessage
                };

                _log.LogError("{InvalidTransaction}", invalidTransaction);
            }
        }
    }
}
