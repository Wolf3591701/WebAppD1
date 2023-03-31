using Autofac.Integration.WebApi;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Employee.Repository.Common;
using Employee.Repository;
using Employee.Service;
using Employee.Service.Common;
using EFEmployee.Repository;
using Employee.DAL;

namespace Day1.WebApi.App_Start
{
    public class DepInject
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
           
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            builder.RegisterType<EFEmployeeRepository>().As<IEmployeeRepository>();
            builder.RegisterType<RentCarContext>().AsSelf();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}