namespace OG.Zoo.Domain.Interfaces.Params.Infirmary
{
    using Entities.Generics;
    using Entities.Params;
    using Generics;
    using System.Threading.Tasks;

    /// <summary>
    /// Infirmary Repository
    /// </summary>
    /// <seealso cref="OG.Zoo.Domain.Interfaces.Generics.IBaseRepository{OG.Zoo.Domain.Entities.Params.Infirmary, System.String}" />
    public interface IInfirmaryRepository: IBaseRepository<Infirmary, string>
    {
        /// <summary>
        /// Gets the with relations.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Infirmary> GetWithRelations(string id);

        /// <summary>
        /// Gets all with relations.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        Task<Paginated<Infirmary>> GetAllWithRelations(int pageIndex, int pageSize, string sortBy, string direction);
    }
}
