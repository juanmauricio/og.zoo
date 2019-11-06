namespace OG.Zoo.Domain.Services.Security.User.Validators
{
    using Entities.Security;
    using FluentValidation;
    using Interfaces.Security.User;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// User general validations
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{OG.Zoo.Domain.Entities.Security.User}" />
    public class UserGeneralValidator : AbstractValidator<User>
    {
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserGeneralValidator" /> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public UserGeneralValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.RuleFor(u => u).MustAsync(this.NotExist).WithMessage(u => $"A user with the same name already exists.");
        }

        /// <summary>
        /// Nots the exist.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        private async Task<bool> NotExist(User user, CancellationToken cancellationToken)
        {
            var result = await this.userRepository.GetBy(user, u => u.Name.Trim().ToUpperInvariant());
            if (result != null) {
                return result.Id == user.Id;
            }
            return result == null;
        }
    }
}