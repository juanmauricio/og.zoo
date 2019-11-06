namespace OG.Zoo.Domain.Interfaces.Params.Animal
{
    using Entities.Params;
    using Generics;

    /// <summary>
    /// Animal Repository
    /// </summary>
    /// <seealso cref="OG.Zoo.Domain.Interfaces.Generics.IBaseRepository{OG.Zoo.Domain.Entities.Params.Animal, System.String}" />
    public interface IAnimalRepository: IBaseRepository<Animal, string>
    {
    }
}
