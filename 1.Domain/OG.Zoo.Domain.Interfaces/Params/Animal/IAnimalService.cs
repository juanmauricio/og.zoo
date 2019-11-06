namespace OG.Zoo.Domain.Interfaces.Params.Animal
{
    using Generics;
    using Entities.Params;

    /// <summary>
    /// Animal Service
    /// </summary>
    /// <seealso cref="OG.Zoo.Domain.Interfaces.Generics.IBaseService{OG.Zoo.Domain.Entities.Params.Animal, System.String}" />
    public interface IAnimalService: IBaseService<Animal, string>
    {
    }
}
