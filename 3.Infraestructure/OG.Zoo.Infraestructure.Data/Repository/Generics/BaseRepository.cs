namespace OG.Zoo.Infraestructure.Data.Repository.Generics
{
    using Domain.Entities.Generics;
    using Domain.Interfaces.Generics;
    using Google.Cloud.Firestore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Utils.Firebase;
    using Utils.Generics;
    using Utils.Objects;

    /// <summary>
    /// Base Repository
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <seealso cref="OG.Zoo.Domain.Interfaces.Generics.IBaseRepository{TEntity, TId}" />
    public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId> where TEntity : Base, new()
    {
        /// <summary>
        /// The database factory
        /// </summary>
        protected readonly IDbFactory DbFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity, TId}"/> class.
        /// </summary>
        /// <param name="dbFactory">The database factory.</param>
        public BaseRepository(IDbFactory dbFactory)
        {
            this.DbFactory = dbFactory;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task Create(TEntity entity)
        {
            var name = typeof(TEntity).Name;
            var db = this.DbFactory.GetDb();
            var document = await db.Collection(name).AddAsync(entity);
            entity.Id = document.Id;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task Update(TEntity entity)
        {
            var name = typeof(TEntity).Name;
            var document = this.DbFactory.GetDb().Collection(name).Document(entity.Id);
            await document.UpdateAsync(entity.AsDictionary());
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task Delete(TId id)
        {
            var name = typeof(TEntity).Name;
            var document = this.DbFactory.GetDb().Collection(name).Document(id.ToString());
            await document.DeleteAsync();
        }

        /// <summary>
        /// Gets by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<TEntity> Get(TId id)
        {
            var name = typeof(TEntity).Name;
            var snapshot = await this.DbFactory.GetDb().Collection(name).Document(id.ToString()).GetSnapshotAsync();
            var entity = snapshot.ConvertTo<TEntity>();
            entity.Id = id.ToString();
            return entity;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var name = typeof(TEntity).Name;
            var snapshot = await this.DbFactory.GetDb().Collection(name).GetSnapshotAsync();
            return snapshot.Documents.Select(d =>
            {
                var entity = d.ConvertTo<TEntity>();
                entity.Id = d.Id;
                return entity;
            });
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="pageIndex">The page.</param>
        /// <param name="pageSize">The items per page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public async Task<Paginated<TEntity>> GetAll(int pageIndex, int pageSize, string sortBy, string direction)
        {
            var sortField = string.IsNullOrEmpty(direction) ? FieldPath.DocumentId : new FieldPath(sortBy.Split('.').Select(s => s.FirstLetterToUpper()).ToArray());
            var name = typeof(TEntity).Name;
            var db = this.DbFactory.GetDb();
            var totalItems = (await db.Collection(name).Select(new string[0]).GetSnapshotAsync()).Count;
            var snapshot = await (direction == FirebaseConstants.OrderByDescending ? db.Collection(name).OrderByDescending(sortField) : db.Collection(name).OrderBy(sortField))
                .Offset((pageIndex) * pageSize).Limit(pageSize).GetSnapshotAsync();
            var results = snapshot.Documents.Select(d =>
            {
                var entity = d.ConvertTo<TEntity>();
                entity.Id = d.Id;
                return entity;
            });
            return new Paginated<TEntity>
            {
                Items = results,
                ItemsPerPage = pageSize,
                Page = pageIndex,
                TotalItems = totalItems
            };
        }

        /// <summary>
        /// Gets the by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<TEntity> GetBy<T>(TEntity entity, Func<TEntity, T> filter) where T : class
        {
            T field = filter(entity);
            var snapshot = await this.DbFactory.GetDb().Collection(typeof(TEntity).Name)
                .GetSnapshotAsync();
            return snapshot.Documents.Select(d =>
            {
                var e = d.ConvertTo<TEntity>();
                e.Id = d.Id;
                return e;
            }).FirstOrDefault(e => field.Equals(filter(e)));
        }

        /// <summary>
        /// Gets all by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAllBy<T>(TEntity entity, Func<TEntity, T> filter) where T : class
        {
            T field = filter(entity);
            var snapshot = await this.DbFactory.GetDb().Collection(typeof(TEntity).Name)
                .GetSnapshotAsync();
            return snapshot.Documents.Select(d =>
            {
                var e = d.ConvertTo<TEntity>();
                e.Id = d.Id;
                return e;
            }).Where(e => field.Equals(filter(e)));
        }
    }
}
