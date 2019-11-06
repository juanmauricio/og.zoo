namespace OG.Zoo.Domain.Interfaces.Security.User
{
    using Generics;
    using Entities.Security;
    using System.Threading.Tasks;

    /// <summary>
    /// User Service
    /// </summary>
    /// <seealso cref="OG.Zoo.Domain.Interfaces.Generics.IBaseService{OG.Zoo.Domain.Entities.Security.User, System.String}" />
    public interface IUserService : IBaseService<User, string>
    {
        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task Login(User user);

        /// <summary>
        /// Gets the user with recovery token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<User> GetUserWithRecoveryToken(string email);

        /// <summary>
        /// Sends the recovery email.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="uri">The URL.</param>
        /// <returns></returns>
        Task SendRecoveryEmail(User user, string uri);

        /// <summary>
        /// Checks the recovery token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<User> CheckRecoveryToken(User user);

        /// <summary>
        /// Sends the update password email.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        Task SendUpdatePasswordEmail(User user, string uri);
    }
}
