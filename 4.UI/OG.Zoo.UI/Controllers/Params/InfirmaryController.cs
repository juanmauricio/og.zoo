namespace OG.Zoo.UI.Controllers.Params
{
    using Application.Interfaces.DTOs;
    using Application.Interfaces.Params;
    using Domain.Entities.Generics;
    using Domain.Entities.Params;
    using Generics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    /// <summary>
    /// Infirmary Controller
    /// </summary>
    /// <seealso cref="string" />
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InfirmaryController : BaseController<Infirmary, string>
    {
        /// <summary>
        /// The infirmary application
        /// </summary>
        private readonly IInfirmaryApplication infirmaryApplication;

        /// <summary>
        /// Initializes a new instance of the <see cref="InfirmaryController" /> class.
        /// </summary>
        /// <param name="application">The application.</param>
        public InfirmaryController(IInfirmaryApplication application) : base(application)
        {
            this.infirmaryApplication = application;
        }

        /// <summary>
        /// Gets the with relations.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("GetWithRelations/{id}")]
        public Task<Response<Infirmary>> GetWithRelations(string id)
        {
            return this.infirmaryApplication.GetWithRelations(id);

        }

        /// <summary>
        /// Gets all with relations.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        [HttpGet("GetAllWithRelations")]
        public Task<Response<Paginated<Infirmary>>> GetAllWithRelations(int pageIndex, int pageSize, string sortBy, string direction)
        {
            return this.infirmaryApplication.GetAllWithRelations(pageIndex, pageSize, sortBy, direction);
        }
    }
}
