using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Test.WebApplication.Commands.FileDeserializer;

namespace Test.WebApplication.Api.Infrastructure.Files
{
    [FileDeserializerTypeAttribute(FileType = FileType.csv)]
    public class CsvTransactionFileDeserializer : IFileDeserializer
    {
        public IEnumerable<T> DeserializeFileContent<T>(Stream stream)
        {
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            return csv.GetRecords<T>().ToList();
        }
    }
}
