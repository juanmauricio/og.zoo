namespace OG.Zoo.Application.Interfaces.Security
{
    using Domain.Entities.Security;
    using DTOs;
    using Generics;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// User Application
    /// </summary>
    /// <seealso cref="OG.Zoo.Application.Interfaces.Generics.IBaseApplication{OG.Zoo.Domain.Entities.Security.User, System.String}" />
    public interface IUserApplication : IBaseApplication<User, string>
    {
        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<Response<User>> Login(User user);

        /// <summary>
        /// Sends the recovery.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        Task<Response<bool>> SendRecovery(string email, string uri);

        /// <summary>
        /// Checks the recovery token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<Response<User>> CheckRecoveryToken(User user);

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<Response<User>> UpdatePassword(User user, string uri);
    }
}
