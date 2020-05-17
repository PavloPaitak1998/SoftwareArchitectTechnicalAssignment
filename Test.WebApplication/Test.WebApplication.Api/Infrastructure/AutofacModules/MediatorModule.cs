using System.Reflection;
using Autofac;
using MediatR;
using Test.WebApplication.Commands.Handlers;
using Test.WebApplication.Queries.Handlers;

namespace Test.WebApplication.Api.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(typeof(FileUploadCommandHandler).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(typeof(TransactionsByCurrencyCodeQueryHandler).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(ctx =>
            {
                var componentContext = ctx.Resolve<IComponentContext>();
                return t => componentContext.Resolve(t);
            });
        }
    }
}
