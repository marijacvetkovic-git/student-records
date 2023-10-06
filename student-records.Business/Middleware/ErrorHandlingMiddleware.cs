using student_records.Business.Middleware.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;


namespace student_records.Business.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate NextRequestDelegate;

        public ErrorHandlingMiddleware(RequestDelegate requestDelegate)
        {
            NextRequestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await ProceedWithServiceCall(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse($"{ExceptionExtension.GetAllMessages(ex)}")));
                return;
            }
        }

        private async Task ProceedWithServiceCall(HttpContext context)
        {
            // Allows using several time the stream in ASP.Net Core
           
            context.Request.EnableBuffering();
            string bodyString = await ReadBodyString(context);
            string path = ReadPath(context);

            try
            {
                if (true)
                {
                    await NextRequestDelegate(context);
                }
            }
            catch (ApiException ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)ex.StatusCode;
                await response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse($"{ExceptionExtension.GetAllMessages(ex)}")));
                return;
            }
        }

        private static string ReadPath(HttpContext context)
        {
            return context.Request.Method + " " + context.Request.Path + context.Request.QueryString;
        }

        private static async Task<string> ReadBodyString(HttpContext context)
        {
            string bodyString = string.Empty;
            // Leave the body open so the next middleware can read it.
            using (var reader = new StreamReader(
                context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 1024,
                leaveOpen: true))
            {
                bodyString = await reader.ReadToEndAsync();
                // Do some processing with body…

                // Reset the request body stream position so the next middleware can read it
                context.Request.Body.Position = 0;
            }

            return bodyString;
        }
    }
}