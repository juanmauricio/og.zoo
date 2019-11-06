namespace OG.Zoo.Application.Services.Security
{
    using Domain.Entities.Security;
    using Domain.Interfaces.Security.User;
    using Generics;
    using Interfaces.DTOs;
    using Interfaces.Security;
    using System.Threading.Tasks;

    /// <summary>
    /// User Application
    /// </summary>
    /// <seealso cref="Interfaces.Generics.IBaseApplication{OG.Zoo.Domain.Entities.Security.User, System.String}" />
    public class UserApplication : BaseApplication<User, string>, IUserApplication
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserApplication"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public UserApplication(IUserService service) : base(service)
        {
            this.userService = service;
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task<Response<User>> Login(User user)
        {
            return ApplicationUtil.Try(async () =>
            {
                await this.userService.Login(user);
                return user;
            });
        }

        /// <summary>
        /// Sends the recovery.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public Task<Response<bool>> SendRecovery(string email, string uri)
        {
            return ApplicationUtil.Try(async () =>
            {
                var user = await this.userService.GetUserWithRecoveryToken(email);
                await this.userService.SendRecoveryEmail(user, uri);
                return true;
            });
        }

        /// <summary>
        /// Checks the recovery token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task<Response<User>> CheckRecoveryToken(User user)
        {
            return ApplicationUtil.Try(() => this.userService.CheckRecoveryToken(user));
        }

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task<Response<User>> UpdatePassword(User user, string uri)
        {
            return ApplicationUtil.Try(async () => {
                var currentUser = await this.userService.CheckRecoveryToken(user);
                currentUser.Password = user.Password;
                await this.userService.Update(currentUser);
                currentUser.Password = string.Empty;
                await this.userService.SendUpdatePasswordEmail(user, uri);
                return currentUser;
            });
        }
    }
}
