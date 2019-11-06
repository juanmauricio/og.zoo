namespace OG.Zoo.Domain.Interfaces.Security.User
{
    using System;
    using System.Threading.Tasks;
    using Entities.Security;
    using Generics;

    /// <summary>
    /// User Repository
    /// </summary>
    /// <seealso cref="OG.Zoo.Domain.Interfaces.Generics.IBaseRepository{OG.Zoo.Domain.Entities.Security.User, System.String}" />
    public interface IUserRepository : IBaseRepository<User, string>
    {
    }
}
