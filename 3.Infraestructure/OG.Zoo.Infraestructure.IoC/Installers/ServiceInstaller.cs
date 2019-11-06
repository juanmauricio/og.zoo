namespace OG.Zoo.Infraestructure.IoC.Installers
{
    using Configuration;
    using Domain.Interfaces.Generics;
    using Domain.Services.Generics;
    using LightInject;
    using OG.Zoo.Domain.Interfaces.Security.User;
    using OG.Zoo.Domain.Services.Security.User;
    using OG.Zoo.Infraestructure.Utils.Injectables.Email;
    using System.Linq;

    /// <summary>
    /// Service Installer
    /// </summary>
    class ServiceInstaller
    {
        /// <summary>
        /// The configure
        /// </summary>
        private IConfigure configure;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInstaller"/> class.
        /// </summary>
        /// <param name="configure">The configure.</param>
        public ServiceInstaller(IConfigure configure)
        {
            this.configure = configure;
        }

        /// <summary>
        /// Installs the specified service registry.
        /// </summary>
        /// <param name="serviceRegistry">The service registry.</param>
        public void Install(IServiceRegistry serviceRegistry)
        {
            var servicesConfig = this.configure.GetServicesConfig();
            serviceRegistry.Register<IUserService>(
                (f) => new UserService(f.GetInstance<IUserRepository>(), f.GetInstance<IEmailService>(), servicesConfig.Key));
            serviceRegistry.RegisterAssembly(
                typeof(BaseService<,>).Assembly, (s, _) =>
                s.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseService<,>)));
        }
    }
}
