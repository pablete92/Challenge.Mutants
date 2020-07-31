using Challenge.Mutants.Infrastructure.Bootstrapers;
using Challenge.Mutants.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Challenge.Mutants.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ICustomLogger logger;

        public ErrorHandlingMiddleware(RequestDelegate next
            , ICustomLogger logger
            )
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            logger.Error(exception.Message, exception);

            var errorModel = new Models.ErrorModel
            {
                Code = -1,
                Message = "The server encountered an internal error."
            };

            if (exception is ProjectException || exception is TimeoutException)
            {
                context.Response.StatusCode = (int)GetHttpStatusCodeByExceptionType(exception);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                errorModel.Module = exception.StackTrace;

                if (exception.InnerException != null)
                {
                    errorModel.Detail = exception.InnerException.Message;
                }
            }

            if (exception is ProjectException)
            {
                var projectException = exception as ProjectException;

                errorModel.Code = context.Response.StatusCode;
                errorModel.Detail = projectException.Detail;
                errorModel.Module = projectException.Module;
                errorModel.Message = projectException.Message;

                if (projectException.ValidationError != null && projectException.ValidationError.Count > 0)
                {
                    errorModel.ValidationError = projectException.ValidationError;
                }
            }

            var result = JsonConvert.SerializeObject(errorModel);

            context.Response.ContentType = "application/json;charset=utf-8";

            return context.Response.WriteAsync(result);
        }

        private HttpStatusCode GetHttpStatusCodeByExceptionType(Exception exception)
        {
            if (exception is BadRequestProjectException)
            {
                return HttpStatusCode.BadRequest;
            }

            if (exception is ForbiddenProjectException)
            {
                return HttpStatusCode.Forbidden;
            }

            if (exception is NotFoundProjectException)
            {
                return HttpStatusCode.NotFound;
            }

            if (exception is TimeoutProjectException || exception is TimeoutException)
            {
                return HttpStatusCode.RequestTimeout;
            }

            if (exception is UnauthorizedAccessProyectException)
            {
                return HttpStatusCode.Unauthorized;
            }

            if (exception is ConflictProjectException)
            {
                return HttpStatusCode.Conflict;
            }

            if (exception is ProjectException)
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
