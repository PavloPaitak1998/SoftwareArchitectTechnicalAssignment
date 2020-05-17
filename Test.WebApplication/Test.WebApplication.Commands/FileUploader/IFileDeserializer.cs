using System.Collections.Generic;
using System.IO;

namespace Test.WebApplication.Commands.FileUploader
{
    public interface IFileDeserializer
    {
        IEnumerable<T> DeserializeFileContent<T>(Stream stream);
    }
}
