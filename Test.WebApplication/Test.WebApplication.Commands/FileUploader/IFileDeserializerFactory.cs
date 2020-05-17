namespace Test.WebApplication.Commands.FileUploader
{
    public interface IFileDeserializerFactory
    {
        IFileDeserializer GetFileDeserializer(FileType fileUploaderType);
    }
}
