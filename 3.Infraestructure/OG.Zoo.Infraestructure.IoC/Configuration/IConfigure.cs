namespace OG.Zoo.Infraestructure.IoC.Configuration
{
    using Configs;

    /// <summary>
    /// Gets the firebase configuration
    /// </summary>
    public interface IConfigure
    {
        /// <summary>
        /// Gets the services configuration.
        /// </summary>
        /// <returns></returns>
        ServicesConfig GetServicesConfig();

        /// <summary>
        /// Gets the email configuration.
        /// </summary>
        /// <returns></returns>
        EmailConfig GetEmailConfig();

        /// <summary>
        /// Gets the firebase configuration.
        /// </summary>
        /// <returns></returns>
        void SetFirebaseConfig();
    }
}
