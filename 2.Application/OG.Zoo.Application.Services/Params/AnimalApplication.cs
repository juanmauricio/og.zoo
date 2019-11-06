namespace OG.Zoo.Application.Services.Params
{
    using Domain.Entities.Params;
    using Domain.Interfaces.Params.Animal;
    using Generics;
    using Interfaces.Params;

    /// <summary>
    /// Animal Application
    /// </summary>
    /// <seealso cref="OG.Zoo.Application.Services.Generics.BaseApplication{OG.Zoo.Domain.Entities.Params.Animal, System.String}" />
    /// <seealso cref="OG.Zoo.Application.Interfaces.Params.IAnimalApplication" />
    public class AnimalApplication : BaseApplication<Animal, string>, IAnimalApplication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalApplication"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public AnimalApplication(IAnimalService service) : base(service)
        {
        }
    }
}
