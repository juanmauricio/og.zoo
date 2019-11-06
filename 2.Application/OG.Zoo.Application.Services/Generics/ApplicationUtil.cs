namespace OG.Zoo.Application.Services.Generics
{
    using Infraestructure.Utils.Exceptions;
    using Interfaces.DTOs;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Aplication Try
    /// </summary>
    public static class ApplicationUtil
    {
        /// <summary>
        /// Tries the specified action.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="action">The action.</param>
        public static async Task<Response<TResult>> Try<TResult>(Func<Task<TResult>> action)
        {
            var response = new Response<TResult>();
            try
            {
                response.Result = await action();
                response.IsSuccess = true;
            }
            catch (AppException aex)
            {
                var ex = aex.InnerException ?? aex;
                response.ExceptionMessage = ex.Message;
                response.ExceptionType = aex.Type;
            }
            catch (Exception ex)
            {
                var e = ex.InnerException ?? ex;
                response.ExceptionMessage = e.Message;
                response.ExceptionType = AppExceptionTypes.Generic;
            }
            return response;
        }
    }
}
