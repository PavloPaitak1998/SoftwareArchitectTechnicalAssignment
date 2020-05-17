using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.WebApplication.Api.Infrastructure.ValidationAttributes;
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

    }
}
