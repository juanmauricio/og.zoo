namespace OG.Zoo.Infraestructure.Utils.Injectables.Email
{
    using FluentEmail.Core;
    using FluentEmail.Core.Models;
    using FluentEmail.Razor;
    using FluentEmail.Smtp;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="OG.Zoo.Infraestructure.Utils.Injectables.Email.IEmailService" />
    public class EmailService: IEmailService
    {

        /// <summary>
        /// The sender
        /// </summary>
        private readonly string sender;

        /// <summary>
        /// The client
        /// </summary>
        private readonly SmtpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService" /> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="user">The user.</param>
        /// <param name="pass">The pass.</param>
        /// <param name="sender">The sender.</param>
        public EmailService(string server, string user, string pass, string sender)
        {
            this.sender = sender;

            this.client = new SmtpClient(server);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(user, pass);
        }

        /// <summary>
        /// Sends the specified destinatary.
        /// </summary>
        /// <param name="destinatary">The destinatary.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public void Send(string destinatary, string subject, string body)
        {
            this.Send(destinatary, subject, body, false);
        }

        /// <summary>
        /// Sends the specified destinatary.
        /// </summary>
        /// <param name="destinataries">The destinataries.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public void Send(IEnumerable<string> destinataries, string subject, string body)
        {
            this.Send(destinataries, subject, body, false);
        }

        /// <summary>
        /// Sends the specified destintary.
        /// </summary>
        /// <param name="destinatary">The destintary.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public void Send(string destinatary, string subject, string body, bool isHtml)
        {
            this.Send(new []{ destinatary }, subject, body, isHtml);
        }

        /// <summary>
        /// Sends the specified destinataries.
        /// </summary>
        /// <param name="destinataries">The destinataries.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public void Send(IEnumerable<string> destinataries, string subject, string body, bool isHtml)
        {
            this.Send(destinataries, subject, body, null, isHtml);
        }

        /// <summary>
        /// Sends the specified destinataries.
        /// </summary>
        /// <param name="destinatary">The destinatary.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public void Send(string destinatary, string subject, string body, dynamic parameters, bool isHtml)
        {
            this.Send(new[] { destinatary }, subject, body, parameters, isHtml);
        }

        /// <summary>
        /// Sends the specified destinataries.
        /// </summary>
        /// <param name="destinataries">The destinataries.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public void Send(IEnumerable<string> destinataries, string subject, string body, dynamic parameters, bool isHtml)
        {
            Email.DefaultRenderer = new RazorRenderer();
            Email.DefaultSender = new SmtpSender(this.client);
            Email.From(this.sender)
                .To(destinataries.Select(destinatary => new Address(destinatary)).ToList())
                .Subject(subject)
                .UsingTemplate(body, parameters, isHtml)
                .Send();
        }
    }
}
