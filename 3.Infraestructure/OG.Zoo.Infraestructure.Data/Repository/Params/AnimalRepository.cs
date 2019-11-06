namespace OG.Zoo.Infraestructure.Data.Repository.Params
{
    using Domain.Entities.Params;
    using Domain.Interfaces.Params.Animal;
    using Generics;

    /// <summary>
    /// Animal Repository
    /// </summary>
    /// <seealso cref="OG.Zoo.Infraestructure.Data.Repository.Generics.BaseRepository{OG.Zoo.Domain.Entities.Params.Animal, System.String}" />
    /// <seealso cref="OG.Zoo.Domain.Interfaces.Params.Animal.IAnimalRepository" />
    public class AnimalRepository : BaseRepository<Animal, string>, IAnimalRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalRepository"/> class.
        /// </summary>
        /// <param name="dbFactory">The database factory.</param>
        public AnimalRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
