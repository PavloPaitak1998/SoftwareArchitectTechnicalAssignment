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
using Test.WebApplication.Commands.FileUploader;
using Test.WebApplication.Commands.Models;
using Test.WebApplication.Common.Dtos;
using Test.WebApplication.Common.Enums;
using Test.WebApplication.UnitOfWork.Interfaces.UnitOfWorks;

namespace Test.WebApplication.Commands.Handlers
{
    public class FileUploadCommandHandler : IRequestHandler<FileUploadCommand, FileUploadResult>
    {
        private readonly IUnitOfTest _unitOfTest;
        private readonly IMapper _mapper;
        private readonly IFileDeserializerFactory _fileDeserializerFactory;
        private readonly ILogger _log;

        public FileUploadCommandHandler(IUnitOfTest unitOfTest, IMapper mapper, IFileDeserializerFactory fileDeserializerFactory, ILogger<FileUploadCommandHandler> log)
        {
            _unitOfTest = unitOfTest;
            _mapper = mapper;
            _fileDeserializerFactory = fileDeserializerFactory;
            _log = log;
        }

        public async Task<FileUploadResult> Handle(FileUploadCommand request, CancellationToken cancellationToken)
        {
            var transactionDtos = new List<TransactionDto>();

            Enum.TryParse(Path.GetExtension(request.File.FileName).Substring(1), out FileType fileType);

            var fileDeserializer = _fileDeserializerFactory.GetFileDeserializer(fileType);

            var records = fileDeserializer.DeserializeFileContent<TransactionModel>(request.File.OpenReadStream());

            if (!TryValidateTransactions(records.ToList(), out var invalidTransactions))
            {
                var res = new FileUploadResult { Status = ResultStatus.Failed };
                res.InvalidTransactions.AddRange(invalidTransactions);

                return res;
            }

            foreach (var record in records)
            {
                var dto = new TransactionDto
                {
                    TransactionIdentificator = record.TransactionIdentificator,
                };

                dto.Amount = decimal.Parse(record.PaymentDetails.Amount);

                dto.TransactionDate = DateTime.ParseExact(record.TransactionDate, "dd/MM/yyyy hh:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture);

                Enum.TryParse(record.PaymentDetails.CurrencyCode, out CurrencyCode code);
                dto.CurrencyCodeId = code;

                Enum.TryParse(record.Status, out TransactionStatusValue statusValue);
                dto.TransactionStatusId = statusValue;

                transactionDtos.Add(dto);
            }

            _unitOfTest.TransactionRepository.CreateTransactions(transactionDtos);
            await _unitOfTest.SaveChangesAsync();

            return new FileUploadResult {Status = ResultStatus.Success};
        }

        private bool TryValidateTransactions(List<TransactionModel> models, out ICollection<InvalidTransaction> invalidTransactions)
        {
            var errorList = new List<string>();

            var validatingTransactions = _mapper.Map<List<InvalidTransaction>>(models);

            invalidTransactions = new List<InvalidTransaction>();

            foreach (var validatingTransaction in validatingTransactions)
            {
                var myType = validatingTransaction.GetType();
                var props = new List<PropertyInfo>(myType.GetProperties());

                foreach (PropertyInfo prop in props)
                {
                    if (prop.Name == nameof(InvalidTransaction.ErrorMessage))
                    {
                        continue;
                    }

                    var propValue = prop.GetValue(validatingTransaction) as string;

                    if (string.IsNullOrEmpty(propValue))
                    {
                        errorList.Add($"The {prop.Name} can not be empty.");
                    }
                }

                if (errorList.Count > 0)
                {
                    validatingTransaction.ErrorMessage = string.Join(" ", errorList);

                    invalidTransactions.Add(validatingTransaction);
                    errorList.Clear();
                }
            }

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

            return invalidTransactions.Count == 0;
        }
    }
}
