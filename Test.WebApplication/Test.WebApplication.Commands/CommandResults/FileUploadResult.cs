using System.Collections.Generic;
using Test.WebApplication.Commands.Models;

namespace Test.WebApplication.Commands.CommandResults
{
    public class FileUploadResult
    {
        public FileUploadResult()
        {
        }

        public FileUploadResult(ResultStatus status, IReadOnlyCollection<InvalidTransaction> invalidTransactions)
        {
            Status = status;
            InvalidTransactions = invalidTransactions;
        }

        public ResultStatus Status { get; set; }
        public IReadOnlyCollection<InvalidTransaction> InvalidTransactions { get; }
    }
}
