namespace Test.WebApplication.Commands.FileDeserializer
{
    public interface IFileDeserializerFactory
    {
        IFileDeserializer GetFileDeserializer(FileType fileUploaderType);
    }
}
