namespace OG.Zoo.Infraestructure.IoC.Installers
{
    using Application.Interfaces.Generics;
    using Application.Services.Generics;
    using Configuration;
    using LightInject;
    using System.Linq;

    /// <summary>
    /// Application Installer
    /// </summary>
    class ApplicationInstaller
    {
        /// <summary>
        /// The configure
        /// </summary>
        private IConfigure configure;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInstaller"/> class.
        /// </summary>
        /// <param name="configure">The configure.</param>
        public ApplicationInstaller(IConfigure configure)
        {
            this.configure = configure;
        }

        /// <summary>
        /// Installs the specified service registry.
        /// </summary>
        /// <param name="serviceRegistry">The service registry.</param>
        public void Install(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterAssembly(typeof(BaseApplication<,>).Assembly,
                (s, _) => s.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseApplication<,>)));
        }
    }
}
