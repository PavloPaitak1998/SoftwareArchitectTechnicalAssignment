using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.WebApplication.Api.Infrastructure.ValidationAttributes;
using Test.WebApplication.Api.ViewModels;
using Test.WebApplication.Commands.CommandResults;
using Test.WebApplication.Commands.Commands;
using Test.WebApplication.Commands.FileDeserializer;
using Test.WebApplication.Common.Enums;
using Test.WebApplication.Queries.Queries;

namespace Test.WebApplication.Api.Controllers
{
    public class TransactionsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TransactionsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        // Post: api/transactions/import
        [HttpPost("import")]
        public async Task<IActionResult> ImportTransactionFileAsync(
            [Required(ErrorMessage = "Please select a file.")]
            [DataType(DataType.Upload)]
            [MaxFileSize(1024)]
            [AllowedExtensions(FileType.xml, FileType.csv)]
            IFormFile file)
        {
            var result = await _mediator.Send(new FileUploadCommand
            {
                File = file
            });

            return result.Status == ResultStatus.Failed
                ? (IActionResult) BadRequest(result.InvalidTransactions)
                : Ok(result);
        }

        // Get: api/transactions/by-currency
        [HttpGet("by-currency")]
        public async Task<IActionResult> GetAllTransactionsByCurrencyAsync([Required] CurrencyCode currencyCode)
        {
            var result = await _mediator.Send(new TransactionsByCurrencyCodeQuery
            {
                CurrencyCode = currencyCode
            });

            return Ok(_mapper.Map<IReadOnlyCollection<TransactionViewModel>>(result));
        }

        // Get: api/transactions/by-status
        [HttpGet("by-status")]
        public async Task<IActionResult> GetAllTransactionsByStatusAsync([Required] TransactionStatusValue statusValue)
        {
            var result = await _mediator.Send(new TransactionsByStatusQuery
            {
                TransactionStatus = statusValue
            });

            return Ok(_mapper.Map<IReadOnlyCollection<TransactionViewModel>>(result));
        }

        // Get: api/transactions/by-date
        [HttpGet("by-date")]
        public async Task<IActionResult> GetAllTransactionsByDateRangeAsync(
              [Required, DataType(DataType.Date)] DateTime fromDate
            , [Required, DataType(DataType.Date)] DateTime toDate)
        {
            var result = await _mediator.Send(new TransactionsByDateRageQuery
            {
                  FromDateTime = fromDate
                , ToDateTime = toDate
            });

            return Ok(_mapper.Map<IReadOnlyCollection<TransactionViewModel>>(result));
        }
    }
}
