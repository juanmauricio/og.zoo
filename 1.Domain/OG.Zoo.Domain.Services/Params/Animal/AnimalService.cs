namespace OG.Zoo.Domain.Services.Params.Animal
{
    using Entities.Params;
    using Interfaces.Params.Animal;
    using Services.Generics;

    /// <summary>
    /// Animal Service
    /// </summary>
    /// <seealso cref="OG.Zoo.Domain.Services.Generics.BaseService{OG.Zoo.Domain.Entities.Params.Animal, System.String}" />
    /// <seealso cref="OG.Zoo.Domain.Interfaces.Params.Animal.IAnimalService" />
    public class AnimalService : BaseService<Animal, string>, IAnimalService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AnimalService(IAnimalRepository repository) : base(repository)
        {
        }
    }
}
