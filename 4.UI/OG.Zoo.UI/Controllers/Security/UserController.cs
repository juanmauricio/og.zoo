namespace OG.Zoo.UI.Controllers.Security
{
    using Application.Interfaces.DTOs;
    using Application.Interfaces.Security;
    using Domain.Entities.Security;
    using Generics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// User Application
    /// </summary>
    /// <seealso cref="OG.Zoo.UI.Controllers.Generics.BaseController{OG.Zoo.Domain.Entities.Security.User, System.String}" />
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User, string>
    {
        /// <summary>
        /// The user application
        /// </summary>
        private readonly IUserApplication userApplication;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="application">The application.</param>
        public UserController(IUserApplication application) : base(application)
        {
            this.userApplication = application;
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public Task<Response<User>> Login([FromBody] User user)
        {
            return this.userApplication.Login(user);
        }

        /// <summary>
        /// Sends the recovery.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("SendRecovery")]
        public Task<Response<bool>> SendRecovery(string email)
        {

            var uri = new Uri(this.Request.GetDisplayUrl()).GetLeftPart(UriPartial.Authority);
            return this.userApplication.SendRecovery(email, uri);
        }

        /// <summary>
        /// Checks the recovery token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("CheckRecoveryToken")]
        public Task<Response<User>> CheckRecoveryToken([FromBody] User user)
        {
            return this.userApplication.CheckRecoveryToken(user);
        }

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("UpdatePassword")]
        public Task<Response<User>> UpdatePassword([FromBody] User user)
        {
            var uri = new Uri(this.Request.GetDisplayUrl()).GetLeftPart(UriPartial.Authority);
            return this.userApplication.UpdatePassword(user, uri);
        }

        /// <summary>
        /// Checks the token.
        /// </summary>
        /// <returns></returns>
        [HttpGet("CheckToken")]
        public Response<bool> CheckToken()
        {
            return new Response<bool>
            {
                IsSuccess = true,
                Result = true
            };
        }
    }
}
