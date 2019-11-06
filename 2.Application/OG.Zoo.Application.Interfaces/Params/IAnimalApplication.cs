namespace OG.Zoo.Application.Interfaces.Params
{
    using Domain.Entities.Params;
    using Generics;

    /// <summary>
    /// Animal Application
    /// </summary>
    /// <seealso cref="OG.Zoo.Application.Interfaces.Generics.IBaseApplication{OG.Zoo.Domain.Entities.Params.Animal, System.String}" />
    public interface IAnimalApplication: IBaseApplication<Animal, string>
    {
    }
}
