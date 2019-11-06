namespace OG.Zoo.Domain.Services.Params.Infirmary
{
    using Entities.Generics;
    using Entities.Params;
    using Generics;
    using Interfaces.Params.Infirmary;
    using System.Threading.Tasks;

    /// <summary>
    /// Infirmary Service
    /// </summary>
    /// <seealso cref="OG.Zoo.Domain.Services.Generics.BaseService{OG.Zoo.Domain.Entities.Params.Infirmary, System.String}" />
    /// <seealso cref="OG.Zoo.Domain.Interfaces.Params.Infirmary.IInfirmaryService" />
    public class InfirmaryService : BaseService<Infirmary, string>, IInfirmaryService
    {
        /// <summary>
        /// The infirmary repository
        /// </summary>
        private readonly IInfirmaryRepository infirmaryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InfirmaryService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public InfirmaryService(IInfirmaryRepository repository) : base(repository)
        {
            this.infirmaryRepository = repository;
        }

        /// <summary>
        /// Gets the with relations.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<Infirmary> GetWithRelations(string id)
        {
            return this.infirmaryRepository.GetWithRelations(id);
        }

        /// <summary>
        /// Gets all with relations.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public Task<Paginated<Infirmary>> GetAllWithRelations(int pageIndex, int pageSize, string sortBy, string direction)
        {
            return this.infirmaryRepository.GetAllWithRelations(pageIndex, pageSize, sortBy, direction);
        }
    }
}
