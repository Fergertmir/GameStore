using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using GameStore.Domain.Concrete;
using GameStore.Domain.Abstract;
using System.Configuration;
using GameStore.WebUI.Infrastructure.Abstract;
using GameStore.Domain.Services;

namespace GameStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        // Injections
        private void AddBindings()
        {
            kernel.Bind<IGameRepository>().To<EFGameRepository>();

            // ConfigurationManager.AppSettings считывает указаный параметр из AppSettings в Web.config (корневой каталог)
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            // Эта конструкция указывает на то что при инициализации конструктора EmailOrderProcessor
            //   в параметр "settings" будет внедрен объект emailSettings.
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            kernel.Bind<IServiceCreator>().To<ServiceCreator>();
            //kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}