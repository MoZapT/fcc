[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LumyCRM.WebApp.Intranet.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LumyCRM.WebApp.Intranet.App_Start.NinjectWebCommon), "Stop")]

namespace LumyCRM.WebApp.Intranet.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ninject.Web.Mvc;
    using System;
    using System.Web;
    using System.Web.Mvc;
    using Ninject.Modules;
    using System.Collections.Generic;
    using FamilyControlCenter.DependencyResolver;
    using System.Web.Http;

    public class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                //  Support WebApi --> WebApiContrib.IoC.Ninject
                NinjectDependencyResolver ninjectResolver = new NinjectDependencyResolver(kernel);
                DependencyResolver.SetResolver(ninjectResolver);    // MVC
                GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel); // API

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var modules = new List<INinjectModule>
            {
                new FamilyModule(),
            };

            kernel.Load(modules);
        }
    }
}
