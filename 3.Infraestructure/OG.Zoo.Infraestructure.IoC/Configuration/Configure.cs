namespace OG.Zoo.Infraestructure.IoC.Configuration
{
    using Microsoft.Extensions.Configuration;
    using OG.Zoo.Infraestructure.IoC.Configuration.Configs;
    using OG.Zoo.Infraestructure.Utils.Firebase;
    using System;

    /// <summary>
    /// Configure
    /// </summary>
    /// <seealso cref="OG.Zoo.Infraestructure.IoC.Configuration.IConfigure" />
    public class Configure: IConfigure
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configure" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Configure(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Gets the firebase configuration.
        /// </summary>
        public void SetFirebaseConfig()
        {
            var section = this.configuration.GetSection(FirebaseConstants.FirebaseConfig);
            Environment.SetEnvironmentVariable(FirebaseConstants.GoogleApplicationCredentials, section["Credentials"]);
            Environment.SetEnvironmentVariable(FirebaseConstants.GoogleProjectId, section["ProjectId"]);
        }

        /// <summary>
        /// Gets the services configuration.
        /// </summary>
        /// <returns></returns>
        public ServicesConfig GetServicesConfig()
        {
            return this.configuration.GetSection(nameof(ServicesConfig)).Get<ServicesConfig>();
        }

        /// <summary>
        /// Gets the email configuration.
        /// </summary>
        /// <returns></returns>
        public EmailConfig GetEmailConfig()
        {
            return this.configuration.GetSection(nameof(EmailConfig)).Get<EmailConfig>();
        }
    }
}
