using Autofac;
using Test.WebApplication.Api.Infrastructure.Files;
using Test.WebApplication.Commands.FileDeserializer;
using Test.WebApplication.UnitOfWork.Implementations.UnitOfWorks;
using Test.WebApplication.UnitOfWork.Interfaces.UnitOfWorks;

namespace Test.WebApplication.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterUnitOfWorks(builder);
            RegisterFiles(builder);
        }

        private void RegisterUnitOfWorks(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfTest>()
                .As<IUnitOfTest>()
                .InstancePerLifetimeScope();
        }

        private void RegisterFiles(ContainerBuilder builder)
        {
            builder.RegisterType<FileDeserializerFactory>()
                .As<IFileDeserializerFactory>()
                .SingleInstance();

            builder.RegisterType<CsvTransactionFileDeserializer>()
                .As<IFileDeserializer>()
                .SingleInstance();

            builder.RegisterType<XmlTransactionFileDeserializer>()
                .As<IFileDeserializer>()
                .SingleInstance();
        }
    }
}
