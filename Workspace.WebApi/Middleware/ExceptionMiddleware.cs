using Newtonsoft.Json;
using System.Net;
using System.Security.Authentication;
using Newtonsoft.Json.Serialization;
using Workspace.Core.ViewModels;

namespace Workspace.WebApi.Middleware
{
    /// <summary>
    /// Перехват и обработка исключений
    /// </summary>
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
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
            catch (Exception exception)
            {
                var errorCodeResponse = GetErrorCode(exception);

                await HandleExceptionAsync(httpContext, errorCodeResponse);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, ErrorCodeResponse errorCodeResponse)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorCodeResponse.HttpStatusCodeNumber;

            var errorViewModel = new ErrorViewModel(errorCodeResponse.Message);

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var json = JsonConvert.SerializeObject(errorViewModel, serializerSettings);

            await context.Response.WriteAsync(json);
        }

        private static ErrorCodeResponse GetErrorCode(Exception exception)
        {
            return exception switch
            {
                InvalidOperationException _ => new ErrorCodeResponse(HttpStatusCode.BadRequest, exception.Message),
                FormatException _ => new ErrorCodeResponse(HttpStatusCode.BadRequest, exception.Message),
                AuthenticationException _ => new ErrorCodeResponse(HttpStatusCode.Forbidden, exception.Message),
                ArgumentException _ => new ErrorCodeResponse(HttpStatusCode.BadRequest, exception.Message),
                NotImplementedException _ => new ErrorCodeResponse(HttpStatusCode.NotImplemented, "Метод не реализован"),
                _ => new ErrorCodeResponse(HttpStatusCode.InternalServerError, exception.Message),
            };
        }

        internal struct ErrorCodeResponse
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="httpStatusCode">Код HTTP-протокола.</param>
            public ErrorCodeResponse(HttpStatusCode httpStatusCode)
            {
                HttpStatusCode = httpStatusCode;
                Message = "Internal server error.";
            }

            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="httpStatusCode">Код HTTP-протокола.</param>
            /// <param name="errorMessage">Сообщение об ошибке.</param>
            public ErrorCodeResponse(HttpStatusCode httpStatusCode, string errorMessage) : this(httpStatusCode)
            {
                Message = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
            }

            /// <summary>
            /// Сообщение.
            /// </summary>
            public string Message { get; private set; }

            /// <summary>
            /// Код HTTP-протокола.
            /// </summary>
            public HttpStatusCode HttpStatusCode { get; private set; }

            /// <summary>
            /// Код HTTP-протокола.
            /// </summary>
            public int HttpStatusCodeNumber => (int)HttpStatusCode;

            /// <summary>
            /// К строке.
            /// </summary>
            /// <returns>_httpStatusCode : _message</returns>
            public override string ToString()
            {
                return $"{(int)HttpStatusCode} : {Message}";
            }
        }
    }
}
