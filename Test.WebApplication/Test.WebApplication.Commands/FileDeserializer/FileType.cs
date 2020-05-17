using System;

namespace Test.WebApplication.Commands.FileDeserializer
{
    public enum FileType
    {
        Unknown,
        xml,
        csv
    }

    public static class FileTypeExtensions
    {
        public static string ToFileFormat(this FileType fileType) =>
            fileType == FileType.Unknown
                ? throw new InvalidOperationException($"File type can not be {nameof(FileType.Unknown)}")
                : "." + fileType;

        public static FileType ToFileType(this string fileExtension)
        {
            var fileType = fileExtension.ToLower().Substring(1);

            return !Enum.TryParse(fileType, out FileType result)
                ? throw new InvalidOperationException($"Invalid or unsupported file type {fileType}")
                : result;
        }
    }
}
