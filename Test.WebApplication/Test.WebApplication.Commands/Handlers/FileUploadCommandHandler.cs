using System;
using System.Collections.Generic;
using MediatR;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
        public FileUploadCommandHandler(IUnitOfTest unitOfTest, IMapper mapper, IFileDeserializerFactory fileDeserializerFactory)
        {
            _unitOfTest = unitOfTest;
            _mapper = mapper;
            _fileDeserializerFactory = fileDeserializerFactory;
        }

        public async Task<FileUploadResult> Handle(FileUploadCommand request, CancellationToken cancellationToken)
        {
            var transactionDtos = new List<TransactionDto>();

            Enum.TryParse(Path.GetExtension(request.File.FileName).Substring(1), out FileType fileType);

            var fileDeserializer = _fileDeserializerFactory.GetFileDeserializer(fileType);

            var records = fileDeserializer.DeserializeFileContent<TransactionModel>(request.File.OpenReadStream());

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
    }
}
