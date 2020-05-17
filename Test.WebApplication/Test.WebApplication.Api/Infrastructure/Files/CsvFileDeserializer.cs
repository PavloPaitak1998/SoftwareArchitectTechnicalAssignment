using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Test.WebApplication.Commands.FileUploader;
using Test.WebApplication.Commands.Models;

namespace Test.WebApplication.Api.Infrastructure.Files
{
    [FileDeserializerTypeAttribute(FileType = FileType.csv)]
    public class CsvFileDeserializer : IFileDeserializer
    {
        public IEnumerable<T> DeserializeFileContent<T>(Stream stream)
        {
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            return csv.GetRecords<T>().ToList();
        }
    }
}
