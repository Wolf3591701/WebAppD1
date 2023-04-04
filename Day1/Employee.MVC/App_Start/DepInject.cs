using Autofac;
using Autofac.Integration.Mvc;
using EFEmployee.Repository;
using Employee.DAL;
using Employee.Repository.Common;
using Employee.Service;
using Employee.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Employee.MVC.App_Start
{
    public class DepInject
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            builder.RegisterType<EFEmployeeRepository>().As<IEmployeeRepository>();
            builder.RegisterType<RentCarContext>().AsSelf();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}