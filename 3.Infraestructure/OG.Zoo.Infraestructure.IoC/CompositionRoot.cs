namespace OG.Zoo.Infraestructure.IoC
{
    using Configuration;
    using Installers;
    using LightInject;
    using Microsoft.Extensions.Configuration;
    using Utils.Injectables.Email;

    public class CompositionRoot
    {
        public void Register(IServiceContainer serviceRegistry, IConfiguration configuration)
        {
            serviceRegistry.Register<IConfigure>((factory) => new Configure(configuration), new PerContainerLifetime());
            var config = serviceRegistry.GetInstance<IConfigure>();
            var email = config.GetEmailConfig();
            serviceRegistry.Register<IEmailService>(
                (f) => new EmailService(email.Server, email.Username, email.Password, email.Sender), new PerRequestLifeTime());
            new RepositoryInstaller(config).Install(serviceRegistry);
            new ServiceInstaller(config).Install(serviceRegistry);
            new ApplicationInstaller(config).Install(serviceRegistry);
        }
    }
}
