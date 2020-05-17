using System.Collections.Generic;
using Test.WebApplication.Commands.Models;

namespace Test.WebApplication.Commands.CommandResults
{
    public class FileUploadResult
    {
        public FileUploadResult()
        {
            InValidTransactions = new List<TransactionModel>();
        }

        public ResultStatus Status { get; set; }
        public List<TransactionModel> InValidTransactions { get; }
    }
}
