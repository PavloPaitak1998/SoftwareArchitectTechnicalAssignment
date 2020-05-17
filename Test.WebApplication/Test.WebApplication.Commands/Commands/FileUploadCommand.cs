using MediatR;
using Microsoft.AspNetCore.Http;
using Test.WebApplication.Commands.CommandResults;

namespace Test.WebApplication.Commands.Commands
{
    public class FileUploadCommand : IRequest<FileUploadResult>
    {
        public IFormFile File { get; set; }
    }
}
