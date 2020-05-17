using System;
using Test.WebApplication.Commands.FileUploader;

namespace Test.WebApplication.Api.Infrastructure.Files
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FileDeserializerTypeAttribute : Attribute
    {
        public FileType FileType { get; set; }
    }
}
