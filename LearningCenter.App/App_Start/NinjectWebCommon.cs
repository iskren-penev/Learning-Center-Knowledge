[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LearningCenter.App.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LearningCenter.App.App_Start.NinjectWebCommon), "Stop")]

namespace LearningCenter.App.App_Start
{
    using System;
    using System.Web;
    using LearningCenter.Data.Interfaces;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Services.Interfaces;
    using LearningCenter.Data;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Services.Implementations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;


    public static class NinjectWebCommon 
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
            kernel.Bind<IUserStore<User>>().To<UserStore<User>>();
            kernel.Bind<UserManager<User>>().ToSelf();

            kernel.Bind<IAuthenticationManager>().ToMethod(c =>
                HttpContext.Current.GetOwinContext().Authentication).InRequestScope();

            kernel.Bind<ICourseService>().To<CourseService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IForumService>().To<ForumService>();
            kernel.Bind<IAdminService>().To<AdminService>();
            kernel.Bind<IUnitsService>().To<UnitsService>();
            kernel.Bind<IQuizService>().To<QuizService>();
            kernel.Bind<ILearningCenterContext>().To<LearningCenterContext>();
        }
    }
}
