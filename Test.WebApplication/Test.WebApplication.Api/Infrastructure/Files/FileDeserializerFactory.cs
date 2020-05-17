using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Test.WebApplication.Commands.FileDeserializer;

namespace Test.WebApplication.Api.Infrastructure.Files
{
    public class FileDeserializerFactory : IFileDeserializerFactory
    {
        private readonly IReadOnlyCollection<IFileDeserializer> _fileDeserializers;

        public FileDeserializerFactory(IReadOnlyCollection<IFileDeserializer> fileDeserializers)
        {
            _fileDeserializers = fileDeserializers;
        }

        public IFileDeserializer GetFileDeserializer(FileType fileType)
        {
            return _fileDeserializers.FirstOrDefault(x =>
                x.GetType().GetCustomAttribute<FileDeserializerTypeAttribute>()?.FileType == fileType);
        }
    }
}
