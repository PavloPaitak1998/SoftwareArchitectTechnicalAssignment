using System.Collections.Generic;
using Test.WebApplication.Commands.Models;

namespace Test.WebApplication.Commands.CommandResults
{
    public class FileUploadResult
    {
        public FileUploadResult()
        {
            InvalidTransactions = new List<InvalidTransaction>();
        }

        public ResultStatus Status { get; set; }
        public List<InvalidTransaction> InvalidTransactions { get; }
    }
}
