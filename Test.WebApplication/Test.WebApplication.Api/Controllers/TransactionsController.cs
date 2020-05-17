using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.WebApplication.Api.Infrastructure.ValidationAttributes;
using Test.WebApplication.Api.ViewModels;
using Test.WebApplication.Commands.Commands;
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
            [AllowedExtensions(new string[] { ".csv", ".xml" })]
            IFormFile file)
        {
            var res = await _mediator.Send(new FileUploadCommand {File = file});

            return Ok(res);
        }

        // Get: api/transactions/by-currency
        [HttpGet("by-currency")]
        public async Task<IActionResult> Get([Required] CurrencyCode currencyCode)
        {
            var res = await _mediator.Send(new TransactionsByCurrencyCodeQuery { CurrencyCode = currencyCode});

            return Ok(_mapper.Map<IReadOnlyCollection<TransactionViewModel>>(res));
        }

        // Get: api/transactions/by-status
        [HttpGet("by-status")]
        public async Task<IActionResult> Get([Required] TransactionStatusValue statusValue)
        {
            var res = await _mediator.Send(new TransactionsByStatusQuery { TransactionStatus = statusValue });

            return Ok(_mapper.Map<IReadOnlyCollection<TransactionViewModel>>(res));
        }

        // Get: api/transactions/by-date
        [HttpGet("by-date")]
        public async Task<IActionResult> Get([Required] [DataType(DataType.Date)] DateTime fromDate, [Required] [DataType(DataType.Date)] DateTime toDate)
        {
            var res = await _mediator.Send(new TransactionsByDateRageQuery { FromDateTime = fromDate, ToDateTime = toDate});

            return Ok(_mapper.Map<IReadOnlyCollection<TransactionViewModel>>(res));
        }

    }
}
