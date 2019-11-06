namespace OG.Zoo.Infraestructure.Data.Repository.Security
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Entities.Security;
    using Domain.Interfaces.Security.User;
    using Generics;

    /// <summary>
    /// User Repository
    /// </summary>
    /// <seealso cref="OG.Zoo.Infraestructure.Data.Repository.Generics.BaseRepository{OG.Zoo.Domain.Entities.Security.User, System.String}" />
    /// <seealso cref="OG.Zoo.Domain.Interfaces.Security.User.IUserRepository" />
    public class UserRepository : BaseRepository<User, string>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dbFactory">The database factory.</param>
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
