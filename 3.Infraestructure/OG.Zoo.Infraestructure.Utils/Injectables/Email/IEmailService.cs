namespace OG.Zoo.Infraestructure.Utils.Injectables.Email
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends the specified destinatary.
        /// </summary>
        /// <param name="destinatary">The destinatary.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        void Send(string destinatary, string subject, string body);

        /// <summary>
        /// Sends the specified destinatary.
        /// </summary>
        /// <param name="destinataries">The destinataries.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        void Send(IEnumerable<string> destinataries, string subject, string body);

        /// <summary>
        /// Sends the specified message to the destinatary.
        /// </summary>
        /// <param name="destinatary">The destinatary.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        void Send(string destinatary, string subject, string body, bool isHtml);

        /// <summary>
        /// Sends the specified destinataries.
        /// </summary>
        /// <param name="destinataries">The destinataries.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        void Send(IEnumerable<string> destinataries, string subject, string body, bool isHtml);

        /// <summary>
        /// Sends the specified destinataries.
        /// </summary>
        /// <param name="destinatary">The destinatary.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        void Send(string destinatary, string subject, string body, dynamic parameters, bool isHtml);

        /// <summary>
        /// Sends the specified message to the destinataries.
        /// </summary>
        /// <param name="destinataries">The destinataries.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        void Send(IEnumerable<string> destinataries, string subject, string body, dynamic parameters, bool isHtml);
    }
}
