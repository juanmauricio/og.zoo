namespace OG.Zoo.Infraestructure.IoC.Installers
{
    using Configuration;
    using Data.Repository.Generics;
    using Domain.Interfaces.Generics;
    using LightInject;
    using System.Linq;

    /// <summary>
    /// Repository Installer
    /// </summary>
    class RepositoryInstaller
    {
        /// <summary>
        /// The configure
        /// </summary>
        private readonly IConfigure configure;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryInstaller"/> class.
        /// </summary>
        /// <param name="configure">The configure.</param>
        public RepositoryInstaller(IConfigure configure)
        {
            this.configure = configure;
        }

        /// <summary>
        /// Installs the specified service registry.
        /// </summary>
        /// <param name="serviceRegistry">The service registry.</param>
        public void Install(IServiceRegistry serviceRegistry)
        {
            this.configure.SetFirebaseConfig();
            serviceRegistry.Register<IDbFactory, DbFactory>(new PerRequestLifeTime());
            serviceRegistry.RegisterAssembly(
                typeof(BaseRepository<,>).Assembly, (s, _) =>
                s.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseRepository<,>)));
        }
    }
}
