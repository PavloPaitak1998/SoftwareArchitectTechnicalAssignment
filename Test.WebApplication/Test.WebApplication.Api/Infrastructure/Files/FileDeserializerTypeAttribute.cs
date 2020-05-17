using System;
using Test.WebApplication.Commands.FileDeserializer;

namespace Test.WebApplication.Api.Infrastructure.Files
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FileDeserializerTypeAttribute : Attribute
    {
        public FileType FileType { get; set; }
    }
}
