using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.WebApplication.Commands.Commands;

namespace Test.WebApplication.Api.Controllers
{
    public class TransactionsController : BaseController
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Post: api/transactions/import
        [HttpPost("import")]
        public async Task<IActionResult> ImportTransactionFileAsync(IFormFile file)
        {
            await _mediator.Send(new FileUploadCommand {File = file});

            return Ok();
        }

    }
}
