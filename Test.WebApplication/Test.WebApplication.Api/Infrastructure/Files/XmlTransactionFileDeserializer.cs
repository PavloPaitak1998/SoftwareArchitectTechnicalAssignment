using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Test.WebApplication.Commands.FileDeserializer;

namespace Test.WebApplication.Api.Infrastructure.Files
{
    [FileDeserializerTypeAttribute(FileType = FileType.xml)]
    public class XmlTransactionFileDeserializer : IFileDeserializer
    {
        public IEnumerable<T> DeserializeFileContent<T>(Stream stream)
        {
            using var reader = new StreamReader(stream);
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute("Transactions"));

            return (List<T>)serializer.Deserialize(reader);
        }
    }
}
