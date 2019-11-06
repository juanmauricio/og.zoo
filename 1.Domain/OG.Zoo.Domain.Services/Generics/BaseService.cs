namespace OG.Zoo.Domain.Services.Generics
{
    using Entities.Generics;
    using FluentValidation;
    using Infraestructure.Utils.Exceptions;
    using Infraestructure.Utils.Generics;
    using Interfaces.Generics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Base Service
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <seealso cref="OG.Zoo.Domain.Interfaces.Generics.IBaseService{TEntity, TId}" />
    public class BaseService<TEntity, TId> : IBaseService<TEntity, TId> where TEntity : Base
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IBaseRepository<TEntity, TId> repository;

        /// <summary>
        /// Gets or sets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        protected AbstractValidator<TEntity> Validator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{TEntity, TId}" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BaseService(IBaseRepository<TEntity, TId> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task Create(TEntity entity)
        {
            this.Validate(entity);
            await this.repository.Create(entity);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task Delete(TId id)
        {
            await this.repository.Delete(id);
        }

        /// <summary>
        /// Gets by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual  async Task<TEntity> Get(TId id)
        {
            return await this.repository.Get(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await this.repository.GetAll();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="pageIndex">The page.</param>
        /// <param name="pageSize">The items per page.</param>
        /// <returns></returns>
        public virtual async Task<Paginated<TEntity>> GetAll(int pageIndex, int pageSize, string sortBy, string direction)
        {
            return await this.repository.GetAll(pageIndex, pageSize, sortBy, direction);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task Update(TEntity entity)
        {
            this.Validate(entity);
            await this.repository.Update(entity);
        }

        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="AppException"></exception>
        /// <exception cref="OG.Zoo.Infraestructure.Utils.Exceptions.AppException"></exception>
        protected void Validate(TEntity entity)
        {
            if (this.Validator != null)
            {
                var result = this.Validator.Validate(entity);
                if (!result.IsValid)
                {
                    throw new AppException(AppExceptionTypes.Validation, result.Errors.FirstOrDefault()?.ErrorMessage);
                }
            }
        }
    }
}
