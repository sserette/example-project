using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Autofac;
using Autofac.Features.Variance;
using Autofac.Integration.WebApi;
using ExampleProject.Command;
using ExampleProject.Query.Queries.Employees;
using ExampleProject.Service.Interfaces;
using ExampleProject.Service.Services;
using MediatR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using Owin;
using ExampleProject.WebApi.Helpers;
using ExampleProject.WebApi.Decorators;
using ExampleProject.Shared;
using ExampleProject.Shared.Interfaces;
using System.Web.Http;
using ExampleProject.Command.Commands;

[assembly: OwinStartup(typeof(ExampleProject.WebApi.Startup))]

namespace ExampleProject.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = ConfigureWebApi();
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(GetEmployeeById.Query).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(AddEmployee.Command).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.RegisterGenericDecorator(
                typeof(ValidationHandlerDecorator<,>),
                typeof(IRequestHandler<,>),
                fromKey: "Handler")
               .Keyed("ValidationDecorated", typeof(IRequestHandler<,>));

            builder.RegisterGenericDecorator(
               typeof(LoggingHandlerDecorator<,>),
               typeof(IRequestHandler<,>),
               fromKey: "ValidationDecorated");

            builder.RegisterType<DebugLogger>().As<ILogger>();

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            builder.RegisterType<EmployeeService>().As<IEmployeeService>();

            string connString = ConfigurationManager.ConnectionStrings["ExampleProject"].ConnectionString;

            builder.Register(c => new ExampleProjectCommandContext(connString))
                .As<ExampleProjectCommandContext>()
                .ExternallyOwned()
                .InstancePerRequest();

            builder.Register(c => new SqlConnection(connString))
                .As<IDbConnection>()
                .ExternallyOwned()
                .InstancePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);

#if DEBUG
            app.UseCors(CorsOptions.AllowAll);
#endif
            app.UseWebApi(config);
        }
        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new NoCacheHeaderFilter());
            config.Filters.Add(new UnHandledExceptionFilterAttribute());
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return config;
        }
    }
}
