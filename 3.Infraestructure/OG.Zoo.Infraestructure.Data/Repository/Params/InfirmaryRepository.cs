namespace OG.Zoo.Infraestructure.Data.Repository.Params
{
    using Domain.Entities.Generics;
    using Domain.Entities.Params;
    using Domain.Interfaces.Params.Infirmary;
    using Generics;
    using Google.Cloud.Firestore;
    using System.Linq;
    using System.Threading.Tasks;
    using Utils.Firebase;
    using Utils.Objects;

    /// <summary>
    /// Infirmary Repository
    /// </summary>
    /// <seealso cref="string" />
    /// <seealso cref="OG.Zoo.Domain.Interfaces.Params.Infirmary.IInfirmaryRepository" />
    public class InfirmaryRepository : BaseRepository<Infirmary, string>, IInfirmaryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InfirmaryRepository"/> class.
        /// </summary>
        /// <param name="dbFactory">The database factory.</param>
        public InfirmaryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// Gets the with relations.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Infirmary> GetWithRelations(string id)
        {
            var db = this.DbFactory.GetDb();
            var snapshot = await db.Collection(typeof(Infirmary).Name).Document(id).GetSnapshotAsync();
            var entity = snapshot.ConvertTo<Infirmary>();
            var snapshotAnimal = await db.Document(entity.IdAnimal).GetSnapshotAsync();
            entity.Animal = snapshotAnimal.ConvertTo<Animal>();
            entity.Animal.Id = snapshotAnimal.Id;
            entity.Id = id;
            return entity;
        }

        /// <summary>
        /// Gets all with relations.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public async Task<Paginated<Infirmary>> GetAllWithRelations(int pageIndex, int pageSize, string sortBy, string direction)
        {
            var sortField = string.IsNullOrEmpty(direction) ? FieldPath.DocumentId : new FieldPath(sortBy.Split('.').Select(s => s.FirstLetterToUpper()).ToArray());
            var name = typeof(Infirmary).Name;
            var db = this.DbFactory.GetDb();
            var totalItems = (await db.Collection(name).Select(new string[0]).GetSnapshotAsync()).Count;
            var snapshot = await (direction == FirebaseConstants.OrderByDescending ? db.Collection(name).OrderByDescending(sortField) : db.Collection(name).OrderBy(sortField))
                .Offset((pageIndex) * pageSize).Limit(pageSize).GetSnapshotAsync();
            var snapshotAnimal = await db.Collection(typeof(Animal).Name).Select(FieldPath.DocumentId, new FieldPath("Name"))
                .GetSnapshotAsync();

            var entities = snapshot.Documents.Select(d =>
            {
                var entity = d.ConvertTo<Infirmary>();
                entity.Id = d.Id;
                return entity;
            }).ToList();

            foreach (var infirmary in entities)
            {
                var key = infirmary.IdAnimal.Split('/').Last();
                var animal = snapshotAnimal.FirstOrDefault(d => d.Id == key)?.ConvertTo<Animal>();
                if (animal != null)
                {
                    animal.Id = key;
                    infirmary.Animal = animal;
                }
            }
            return new Paginated<Infirmary>
            {
                Items = entities,
                ItemsPerPage = pageSize,
                Page = pageIndex,
                TotalItems = totalItems
            };
        }
    }
}
