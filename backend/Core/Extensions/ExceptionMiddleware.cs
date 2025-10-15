using Core.Entities;
using Core.Utilities.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        public Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> errors;

            if (e.GetType() == typeof(ValidationException))
            {
                message = e.Message;
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    Errors = errors,
                    Message = message,
                    StatusCode = 400
                }.ToString());
            }

            if (e is AuthorizationException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                message = e.Message;

                return httpContext.Response.WriteAsync(new ErrorDetails
                {
                    Message = message,
                    StatusCode = httpContext.Response.StatusCode
                }.ToString());
       
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                Message = message,
                StatusCode = httpContext.Response.StatusCode
            }.ToString());
        }
    }
}
